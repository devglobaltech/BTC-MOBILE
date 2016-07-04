DELIMITER $$

CREATE
    /*[DEFINER = { user | CURRENT_USER }]*/
    PROCEDURE `proc_ins_bulto_leido`(	p_idpedido	INT(11),
						p_nro_remito	VARCHAR(100),
						p_nro_bulto	VARCHAR(100),
						p_cant_bultos	INT(11)
					)

    BEGIN
	DECLARE v_existe_bulto		INT(11);
	DECLARE v_total_bultos		INT(11);
	DECLARE v_dummy			VARCHAR(2);
	DECLARE done 			INT;
	DECLARE v_idevento_pedido	VARCHAR(100);


	DECLARE cur1 CURSOR FOR 
	SELECT idevento FROM evento_operacion WHERE idoperacion='EPROCMOB001' AND idempresa=fx_get_idempresa(p_idpedido);
	
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done=1;    
		
	SELECT 	COUNT(*)INTO v_existe_bulto
	FROM	mob_bultos_leidos
	WHERE	idpedido=p_idpedido
		AND nro_remito=p_nro_remito
		AND nro_bulto=p_nro_bulto;
		
	IF v_existe_bulto=0 THEN
		INSERT INTO mob_bultos_leidos (idpedido, nro_remito, nro_bulto)
		VALUES(p_idpedido, p_nro_remito, p_nro_bulto);
	END IF;
	
	SELECT 	COUNT(*)INTO v_total_bultos
	FROM	mob_bultos_leidos
	WHERE	idpedido=p_idpedido
		AND nro_remito=p_nro_remito;
		
	IF v_total_bultos=p_cant_bultos THEN

			
		OPEN cur1;
		
		igmLoop: LOOP
		FETCH cur1 INTO v_idevento_pedido;
		IF done = 1 THEN LEAVE igmLoop; END IF;	
		
			CALL proc_insertar_evento(p_idpedido,v_idevento_pedido,DATE_FORMAT(SYSDATE(), '%d/%m/%Y %H:%i'));
			
		END LOOP igmLoop;
		CLOSE cur1;

		-- Borro los registros para que no crezca la tabla.
		DELETE FROM mob_bultos_leidos WHERE idpedido=p_idpedido;
		
	END IF;
    END$$

DELIMITER ;