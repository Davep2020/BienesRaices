$(function () {

    validacion();

});

function validacion() {
    $("#frmModificaCuenta").validate({

        rules: {
            //Etique name para validar
            Contrasena_CR: {
                required: true,
                minlength: 2,
                maxlength: 50
            }
        }

    });
}