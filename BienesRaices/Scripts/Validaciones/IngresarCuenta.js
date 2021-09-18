$(function () {

    validacion();

});

function validacion() {
    $("#frmNuevaCuenta").validate({

        rules: {
            //Etique name para validar
            Usuario_CR: {
                required: true,
                minlength: 2,
                maxlength: 50
            },
            Contrasena_CR: {
                required: true,
                minlength: 2,
                maxlength: 50
            }
        }

    });
}