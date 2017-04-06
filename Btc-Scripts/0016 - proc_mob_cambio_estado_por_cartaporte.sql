DELIMITER $$

USE `transport_desarrollo`$$

DROP PROCEDURE IF EXISTS `proc_mob_cambio_estado_por_cartaporte`$$

CREATE DEFINER=`fohet`@`%` PROCEDURE `proc_mob_cambio_estado_por_cartaporte`(	p_idcarta_porte	INT(11),
							p_idoperacion	VARCHAR(100))
BEGIN
DECLARE v_dummy			VARCHAR(2);
DECLARE done 			INT(11);
DECLARE v_idevento_pedido	VARCHAR(100);
DECLARE v_idpedido		INT(11);
DECLARE cur CURSOR FOR 
SELECT	p.idpedido,
	idevento	
FROM 	pedido p INNER JOIN empresa e 			ON (p.idempresa = e.idempresa)
	INNER JOIN carta_porte_pedido cpp		ON (p.idpedido=cpp.idpedido)
	INNER JOIN carta_porte cpo			ON (cpp.idcarta_porte=cpo.idcarta_porte)
	INNER JOIN evento_operacion eo			ON (p.idempresa=eo.idempresa)
WHERE	cpo.idcarta_porte=p_idcarta_porte
	AND eo.idoperacion=p_idoperacion
	AND p.idtipo_operacion_actual IN(SELECT valor FROM parametros_procesos WHERE proceso_id='DESPACHO_CARTADEPORTE' AND parametro_id='DESPACHOS');
DECLARE CONTINUE HANDLER FOR NOT FOUND SET done=1;    
OPEN cur;
igmLoop: LOOP
FETCH cur INTO v_idpedido, v_idevento_pedido;
IF done = 1 THEN LEAVE igmLoop; END IF;	
	-- CALL proc_insertar_evento(v_idpedido,v_idevento_pedido,DATE_FORMAT(SYSDATE(), '%d/%m/%Y %H:%i'));
	CALL proc_ins_carga_evento_operacion(v_idpedido,p_idoperacion,DATE_FORMAT(SYSDATE(), '%d/%m/%Y %H:%i'));
	
END LOOP igmLoop;
CLOSE cur;
END$$

DELIMITER ;