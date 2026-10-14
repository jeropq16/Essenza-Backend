# Application: catálogo inicial

Esta capa coordina los casos de uso del catálogo. Solo depende de Domain.

- DTOs: datos de entrada y salida. No son entidades ni tablas.
- Interfaces: contratos de los servicios que podrá usar la API.
- Services: consultan repositorios, comprueban las relaciones, llaman a las entidades y devuelven DTOs.
- Exceptions: NotFoundException indica que no existe el recurso solicitado.

## Flujo de ejemplo

La API entregará un CreateProductDto a ProductService.CreateAsync.
El servicio comprueba que existen la marca y la categoría, crea Product
(que valida sus reglas), pide al repositorio que lo guarde y devuelve ProductDto.

Los constructores reciben interfaces de repositorio. Infrastructure implementará
esas interfaces y la API registrará servicios y repositorios en la inyección de dependencias.

## Comportamiento

- Una búsqueda individual o actualización de un recurso inexistente lanza NotFoundException.
- Las listas generales pueden estar vacías.
- Listar variantes o imágenes exige que el producto exista; si existe sin hijos, devuelve una lista vacía.
- Las reglas de nombres, precios, género y existencias siguen en Domain y lanzan ArgumentException o sus derivadas.
- Cada AddAsync y UpdateAsync del repositorio debe persistir antes de terminar, según los contratos de Domain.
- UpdateProductDto actualiza precio, descuento y ambos indicadores juntos; null en DiscountPrice elimina el descuento.
- UpdateProductVariantStockDto reemplaza el stock absoluto; no representa una venta ni un descuento concurrente de existencias.
- GetAllAsync de productos devuelve activos e inactivos para administración. Un catálogo público deberá filtrar los activos.
- Las imágenes se reciben en el orden que garantiza IProductImageRepository.
- No hay eliminación ni edición de nombres porque las entidades y contratos actuales no ofrecen esas operaciones.

No se agregaron paquetes, base de datos, autenticación ni controladores.
Las carpetas Options, Security y Validators se reservan para necesidades futuras;
las validaciones actuales del dominio se reutilizan sin duplicarlas.
