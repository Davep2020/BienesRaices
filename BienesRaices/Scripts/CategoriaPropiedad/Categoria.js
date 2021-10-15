//$(function () {
   
//});

function cambiarCategoria(Id_Categoria_P) {

    var id = Id_Categoria_P;

    document.getElementById("id").value = id;
    //cargaInputCategoria(id);

    $.ajax({
        url: '/Index/Index', // Url
        data: {
            Id_Categoria_P: id // Parámetros
        },
        type: "Post"  // Verbo HTTP
    })
    // Se ejecuta si todo fue bien.
            .done(function (result) {
                if (result != null) {
                }
                else {
                }
            })
            // Se ejecuta si se produjo un error.
            .fail(function (xhr, status, error) {

            })
            // Hacer algo siempre, haya sido exitosa o no.
            .always(function () {

            });

}

//$(function () {
//    $('#id-boton').click(function () {
//        e.preventDefault();

//        $.ajax({
//            url: "@Url.Action("PaymentWithPaypal", "PayPal")", // Url
//            data: {
//                price: $("#id-price").val(), // Parámetros
//                product: $("#id-product").val()
//            },
//            type: "post"  // Verbo HTTP
//        })
//            // Se ejecuta si todo fue bien.
//            .done(function (result) {
//                if (result != null) {
//                }
//                else {
//                }
//            })
//            // Se ejecuta si se produjo un error.
//            .fail(function (xhr, status, error) {

//            })
//            // Hacer algo siempre, haya sido exitosa o no.
//            .always(function () {

//            });
//    });
//});


//function cargaInputCategoria(pId) {



//    var url = '/Index/Index';
//    var parametros = {
//        Id_Categoria_P: pId
//    };
//    $.ajax({
//        url: url,
//        dataType: 'json',
//        type: 'post',
//        contentType: 'application/json',
//        data: JSON.stringify(parametros),
//        success: function (data, textStatus, jQxhr) {
            
//        },
//        error: function (jQxhr, textStatus, errorThrown) {
//            alert(errorThrown);
//        },
//    });
//}