CREATE VIEW view_mob_restantes_hdr
AS
SELECT	p.nro_remito             	AS nro_remito,
	p.cantidad_bultos        	AS cantidad_bultos,
	vh.patente		 	AS patente,
	cpo.idcarta_porte 		AS hdr,
	p.idpedido        		AS idpedido,
	COUNT(bl.nro_bulto)		AS restan			
FROM 	pedido p JOIN empresa e 			ON (p.idempresa = e.idempresa)
	JOIN provincia prov				ON (p.idprovincia = prov.idprovincia)
	JOIN dock d					ON (p.iddock = d.iddock)
	JOIN codigo_postal cp				ON (p.idcodigo_postal = cp.idcodigo_postal)
	LEFT JOIN tipo_servicio ts			ON (p.idtipo_servicio = ts.idtipo_servicio)
	INNER JOIN carta_porte_pedido cpp		ON (p.idpedido=cpp.idpedido)
	INNER JOIN carta_porte cpo			ON (cpp.idcarta_porte=cpo.idcarta_porte)
	INNER JOIN vehiculo vh				ON (cpo.idvehiculo=vh.idvehiculo)
	LEFT JOIN mob_carta_porte_bultos_leidos bl	ON (p.idpedido=bl.idpedido)
WHERE	p.idstatus IN(SELECT idstatus FROM evento_operacion WHERE idoperacion='MOB002')
GROUP BY
	p.nro_remito,
	p.cantidad_bultos,
	vh.patente,
	cpo.idcarta_porte,
	p.idpedido
HAVING 	COUNT(bl.nro_bulto)<p.cantidad_bultos