$(function () {
    obtenerRegistrosFactura();
});


/// funcion que obtiene los registros
// del metodo del controlador
// RetornaPersonasLista()
function obtenerRegistrosFactura() {
    /////construir la dirección del método del servidor
    var urlMetodo = '/Propiedad/ReportesVentas'
    var parametros = {};
    var funcion = creaGridKendo;
    ///ejecuta la función $.ajax utilizando un método genérico
    //para no declarar toda la instrucción siempre
    ejecutaAjax(urlMetodo, parametros, funcion);
}
///encargada de crear el grid de kendo y mostrar
//los datos obtenidos al llamar al método
// RetornaListaClientes
function creaGridKendo(data) {


    $("#divKendoGrid").kendoGrid({
        dataSource: {
            data: data.resultado,
            pageSize: 10
        },
        pageable: true,
        filterable: {
            messages: {
                info: "Título:", // sets the text on top of the filter menu
                filter: "Filtrar", // sets the text for the "Filter" button
                clear: "Limpiar", // sets the text for the "Clear" button

                // when filtering boolean numbers
                isTrue: "Verdadero", // sets the text for "isTrue" radio button
                isFalse: "Falso", // sets the text for "isFalse" radio button

                //changes the text of the "And" and "Or" of the filter menu
                and: "Y",
                or: "Ó"
            },
            operators: {
                //filter menu for "string" type columns
                string: {
                    eq: "Igual a",
                    neq: "Diferente de",
                    startswith: "Empieza en",
                    contains: "Contiene",
                    endswith: "Termina en"
                },
                //filter menu for "number" type columns
                //number: {
                //    eq: "Igual a",
                //    neq: "Diferente de",
                //    gte: "Mayor que ou igual a",
                //    gt: "Mair que",
                //    lte: "Menor que ou igual a",
                //    lt: "Menor que"
                //},
                //filter menu for "date" type columns
                //date: {
                //    eq: "Igual a",
                //    neq: "Diferente de",
                //    gte: "Maior que ou igual a",
                //    gt: "Mair que",
                //    lte: "Menor que ou igual a",
                //    lt: "Menor que"
                //},
            },
        },
        columns: [
            {
                field: 'Nombre_P',
                title: 'Nombre Propiedad'
            }, {
                field: 'Precio',
                title: 'Precio'
            },
            {
                field: 'Nombre_CA',
                title: 'Categoria'
            },
            {
                field: 'NombreCompleto_CO',
                title: 'Nombre del comprador'
            },
            {
                field: 'Telefono_CO',
                title: 'Teléfono'
            },
        ],

        filterable: true,
        toolbar: ["excel", "pdf"],
        excel: {
            fileName: "Reporte_Citas_Cliente.xlsx",
            filterable: true,
            allPages: true
        },
        pdf: {
            fileName: "Reporte_Citas_Cliente.pdf",
            date: new Date(),

        }



    });
}