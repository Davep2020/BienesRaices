function validarImagen() {
    var fileSize = $('#imagen')[0].files[0].size;
    var siezekiloByte = parseInt(fileSize / 1024);
    if (siezekiloByte > $('#imagen').attr('size')) {
        alert("Imagen muy grande");
        return false;
    }
}