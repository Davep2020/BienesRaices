function cambiarEstado(Id_Credenciales_CR) {
    console.log(Id_Credenciales_CR);

    var id = Id_Credenciales_CR;

    document.getElementById("cuenta").value = id;
}

function ActivarEmpresa(Id_Credenciales_CR) {
    console.log(Id_Credenciales_CR);
    var id = Id_Credenciales_CR;

    document.getElementById("activar").value = id;
}
