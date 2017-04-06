DELIMITER $$

USE `transport_desarrollo`$$

DROP PROCEDURE IF EXISTS `proc_mob_confirmar_expedicion_carta_porte`$$

CREATE DEFINER=`fohet`@`%` PROCEDURE `proc_mob_confirmar_expedicion_carta_porte`(p_idcarta_porte	INT(11))
BEGIN
	DECLARE v_dummy			VARCHAR(2);
	DECLARE done 			INT;
	DECLARE v_idevento_pedido	VARCHAR(100);
	DECLARE v_idpedido		INT(11);
	DECLARE cur1 CURSOR FOR 
	SELECT	p.idpedido,
		idevento	
	FROM 	pedido p JOIN empresa e 			ON (p.idempresa = e.idempresa)
		INNER JOIN carta_porte_pedido cpp		ON (p.idpedido=cpp.idpedido)
		INNER JOIN carta_porte cpo			ON (cpp.idcarta_porte=cpo.idcarta_porte)
		INNER JOIN evento_operacion eo			ON (p.idempresa=eo.idempresa)
		INNER JOIN mob_carta_porte_bultos_leidos m	ON (p.idpedido=m.idpedido)
	WHERE	cpo.idcarta_porte=p_idcarta_porte
		AND eo.idoperacion='EPROCMOB002';
	
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done=1;    
	OPEN cur1;
	
	igmLoop: LOOP
	FETCH cur1 INTO v_idpedido, v_idevento_pedido;
	IF done = 1 THEN LEAVE igmLoop; END IF;	
	
		-- CALL proc_insertar_evento(v_idpedido,v_idevento_pedido,DATE_FORMAT(SYSDATE(), '%d/%m/%Y %H:%i'));
		CALL proc_ins_carga_evento_operacion(v_idpedido,'EPROCMOB002',DATE_FORMAT(SYSDATE(), '%d/%m/%Y %H:%i'));
		
	END LOOP igmLoop;
	CLOSE cur1;
	DELETE FROM mob_carta_porte_bultos_leidos WHERE idcarta_porte=p_idcarta_porte;
	
    END$$

DELIMITER ;