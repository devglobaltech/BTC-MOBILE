CREATE VIEW `view_mob_carga_hdr` 
AS 
	SELECT	p.nro_remito             AS nro_remito,
		p.cantidad_bultos        AS cantidad_bultos,
		vh.patente		 AS patente
	FROM 	pedido p JOIN empresa e 		ON (p.idempresa = e.idempresa)
		JOIN provincia prov			ON (p.idprovincia = prov.idprovincia)
		JOIN dock d				ON (p.iddock = d.iddock)
		JOIN codigo_postal cp			ON (p.idcodigo_postal = cp.idcodigo_postal)
		LEFT JOIN tipo_servicio ts		ON (p.idtipo_servicio = ts.idtipo_servicio)
		INNER JOIN carta_porte_pedido cpp	ON (p.idpedido=cpp.idpedido)
		INNER JOIN carta_porte cpo		ON (cpp.idcarta_porte=cpo.idcarta_porte)
		INNER JOIN vehiculo vh			ON (cpo.idvehiculo=vh.idvehiculo)
	WHERE	cpo.idstatus=cpo.idstatus
;
