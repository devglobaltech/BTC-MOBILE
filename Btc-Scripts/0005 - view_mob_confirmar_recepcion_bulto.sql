DELIMITER $$

ALTER VIEW `view_mob_confirmar_recepcion_bulto` 
AS 
SELECT	`p`.`idpedido`               AS `idpedido`,
	`p`.`nro_remito`             AS `nro_remito`,
	`p`.`nombre`                 AS `nombre`,
	`p`.`direccion`              AS `direccion`,
	`p`.`idcodigo_postal`        AS `idcodigo_postal`,
	`p`.`idlocalidad`            AS `idlocalidad`,
	`prov`.`descripcion`         AS `descripcion`,
	`p`.`cantidad_bultos`        AS `cantidad_bultos`,
	`e`.`codigo_empresa`         AS `codigo_empresa`,
	`p`.`idreferencia`           AS `idreferencia`,
	`p`.`fecha_estimada_entrega` AS `fecha_estimada_entrega`,
	`d`.`descripcion`            AS `dock_nom`,
	`cp`.`zona`                  AS `zona`,
	`ts`.`descripcion`           AS `tipo_servicio`,
	`ex`.`razon_social`	    	 AS `expreso`
FROM 	`pedido` `p` JOIN `empresa` `e` 	ON (`p`.`idempresa` = `e`.`idempresa`)
	JOIN `provincia` `prov`			ON (`p`.`idprovincia` = `prov`.`idprovincia`)
	JOIN `dock` `d`				ON (`p`.`iddock` = `d`.`iddock`)
	JOIN `codigo_postal` `cp`		ON (`p`.`idcodigo_postal` = `cp`.`idcodigo_postal`)
	LEFT JOIN `tipo_servicio` `ts`		ON (`p`.`idtipo_servicio` = `ts`.`idtipo_servicio`)
	LEFT JOIN `expreso` `ex`		ON (`p`.`idexpreso` = `ex`.`idexpreso`)
WHERE	`p`.`idstatus` IN(SELECT `idstatus` FROM `evento_operacion` WHERE `idoperacion`='MOB001')
$$

DELIMITER ;