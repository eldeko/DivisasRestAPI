#!/bin/bash

procDocument=$(w3m -dump "http://www.mercadodeliniers.com.ar/dll/hacienda1.dll/haciinfo000002?txtFECHAINI=10/05/2020&txtFECHAFIN=10/05/2020&CP=&LISTADO=SI")
procDocument=$(echo "$procDocument" | awk 'NR > 7 { print }' | head -n -8 | sed '/-/,+1 d' | grep -v 'Totales')
procDocument=$(echo "$procDocument" | awk '{for(i=NF;i>1;i=i-1) printf "%s ", $i; printf "%s\n", $1}'| sed 's/\$//g'| sed 's/\.//g'| sed 's/\,/./g')

IFS=$'\n'
CategoriaHeader="{ "nombreCategoria": null, "precioPromedioCategoria": null, "totalCabezasCategoria": null, "totalImporteCategoria": null, "totalKgsCategoria": null, "kgsPromedioCategoria": null, "subcategorias": ["
Categoria=""

#echo $CategoriaHeader

for lines in $procDocument; do


					NombreSubCategoria=$(echo "$lines" | awk  '{for (i=NF;i>8;i--) print $i}')
				  Categoria=$(echo $NombreSubCategoria | awk  -F' ' '{print $1}')
						 KgsProm=$(echo "$lines" | awk  '{print $1}')
							 Kgs=$(echo "$lines" | awk  '{print $2}')
						 Importe=$(echo "$lines" | awk  '{print $3}')
 						 Cabezas=$(echo "$lines" | awk  '{print $4}')
					     Mediana=$(echo "$lines" | awk  '{print $5}')
						Promedio=$(echo "$lines" | awk  '{print $6}')
						  Maximo=$(echo "$lines" | awk  '{print $7}')
						  Minimo=$(echo "$lines" | awk  '{print $8}')
	  

salidaSubCategoria=$(echo \{NombreSubCategoria: \"""$NombreSubCategoria""\", NombreCategoria : \"""$Categoria""\", Minimo : \"""$Minimo""\", Maximo : \"""$Maximo""\",Promedio: \"""$Promedio""\", Mediana : \"""$Mediana""\", Cabezas : \"""$Cabezas""\" ,Importe : \"""$Importe""\", Kgs : \"""$Kgs""\", KgsProm : \"""$KgsProm""\" \}, )

salida="$salida$salidaSubCategoria"


done
echo {SubCategorias:["${salida::-1}" ]}
