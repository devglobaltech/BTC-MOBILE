DELIMITER $$
CREATE
PROCEDURE proc_ins_carta_porte_bulto(	p_idcarta_porte	INT(11),
					p_idpedido	INT(11),
					p_nro_bulto	VARCHAR(100)
					)
BEGIN
	DECLARE v_ctn_lectura	INT(11);
	
	SELECT 	COUNT(*) INTO v_ctn_lectura
	FROM	mob_carta_porte_bultos_leidos
	WHERE	idcarta_porte=p_idcarta_porte;

	IF(v_ctn_lectura=0) THEN
	
		INSERT INTO mob_carta_porte_bultos_leidos (idcarta_porte, idpedido, nro_bulto, fecha)
		VALUES(p_idcarta_porte, p_idpedido, p_nro_bulto, SYSDATE());
		
	END IF;
	
END$$

DELIMITER ;