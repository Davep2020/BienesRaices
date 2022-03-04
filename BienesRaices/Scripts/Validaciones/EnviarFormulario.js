$(function () {

    validacion();

});

function validacion() {
    $("#formContact").validate({

        rules: {
            NombreCompleto_CO: {
                required: true,
                minlength: 2,
                maxlength: 50
            },
            Telefono_CO: {
                required: true,
                minlength: 8,
                maxlength: 8,
                number: true
            },
            Correo_CO: {
                required: true,
                minlength: 4,
                maxlength: 25
            },
            Comentario_CO: {
                required: true,
                minlength: 5,
                maxlength: 500
            }
        }

    });
}