
CREATE
    VIEW `btc`.`view_mob_informe_faltantes` 
    AS
    
SELECT	p.idpedido               	AS idpedido,
	p.cantidad_bultos  - 
	IFNULL(COUNT(bl.nro_bulto),0)	AS bultos_faltantes,
	p.nro_remito             	AS nro_remito
FROM 	pedido p JOIN empresa e		ON (p.idempresa = e.idempresa)
	JOIN provincia prov	        ON (p.idprovincia = prov.idprovincia)
	JOIN dock d			ON (p.iddock = d.iddock)
	JOIN codigo_postal cp		ON (p.idcodigo_postal = cp.idcodigo_postal)
	LEFT JOIN tipo_servicio ts	ON (p.idtipo_servicio = ts.idtipo_servicio)
	LEFT JOIN mob_bultos_leidos bl	ON (p.idpedido=bl.idpedido)
WHERE	p.idstatus IN(SELECT idstatus FROM evento_operacion WHERE idoperacion='MOB001')
GROUP BY
	p.idpedido, p.cantidad_bultos, p.nro_remito
ORDER BY
	p.idpedido;
