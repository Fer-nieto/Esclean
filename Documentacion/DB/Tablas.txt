|  # | Tabla                      | Descripción                                                                                                                                  |
| -: | -------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------- |
|  1 | `roles`                    | Catálogo de roles y niveles de acceso de los usuarios.                                                                                       |
|  2 | `users`                    | Usuarios de Esclean. Contiene número de empleado, autenticación, rol y estado de la cuenta.                                                  |
|  3 | `production_lines`         | Catálogo de líneas de producción, por ejemplo `AG01` a `AG08`.                                                                               |
|  4 | `locations`                | Catálogo de ubicaciones como `CLEANING`, `PISO`, `CUARTERIAS`, `REPARACION` y `SCRAP`.                                                       |
|  5 | `stencils`                 | Inventario maestro de esténciles. Contiene Steel No., nombre, tipo, lado, producto, modelo de uso, fabricante, ubicación y condición actual. |
|  6 | `stencil_comments`         | Histórico de comentarios asociados a cada esténcil. Los comentarios anteriores no se sobrescriben.                                           |
|  7 | `stencil_movements`        | Histórico de entradas, salidas y transferencias de esténciles entre ubicaciones y líneas.                                                    |
|  8 | `stencil_damage_reports`   | Histórico de daños reportados en esténciles, incluyendo tipo de daño, descripción, línea, usuario y fecha.                                   |
|  9 | `stencil_damage_evidence`  | Referencias a fotografías almacenadas en el bucket y asociadas a los reportes de daño.                                                       |
| 10 | `stencil_wash_movements`   | Registro de inicio y fin de los lavados de esténciles. Permite conocer cuáles están actualmente en lavado y calcular duración.               |
| 11 | `stencil_inspections`      | **En espera.** La información de inspección y tensiones se buscará obtener directamente desde la base de datos de SUNMENTA.                  |
| 12 | `trays`                    | Catálogo maestro de tipos de charola, modelos compatibles, proveedor y capacidad/espacios.                                                   |
| 13 | `tray_movements`           | Histórico de movimientos de charolas entre ubicaciones. También registra charolas dañadas enviadas a `SCRAP`.                                |
| 14 | `tray_inventory`           | Inventario actual de charolas por ubicación. Se actualiza a partir de los movimientos.                                                       |
| 15 | `squeegee_holders`         | Inventario estático de porta navajas por tipo/tamaño y cantidad total.                                                                       |
| 16 | `squeegee_blades`          | Inventario actual de navajas por tipo/tamaño, incluyendo cantidad disponible y nivel mínimo.                                                 |
| 17 | `squeegee_blade_movements` | Histórico de entradas, salidas y scrap de navajas. Sus movimientos modifican el inventario disponible.                                       |
