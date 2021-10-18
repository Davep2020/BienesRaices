$(document).ready(function () {
    var nombreP = $("#hdProvinciaID").val();
    var idPro = $("#hddProvinciaID").val();
    getData(
        "https://ubicaciones.paginasweb.cr/provincias.json",
        function (data) {
            arrayToOptions(data, $("#provincias"), 'Provincias', idPro, nombreP);
            var idProvincia = $('#hddProvinciaID').val();
            var idCanton = $('#hddCantonID').val();
            getCantones(idProvincia);
            getDistritos(idCanton, idProvincia);
            var id = $("#Id_Categoria_P").val();
            mostrar(id);
            
            $('#1').val($("#provincias option:selected").text());
            
            
            var l1 = $('#1').val();
            
            
        }
    );

});

function initMap() {
    geocoder = new google.maps.Geocoder();

    google.maps.event.addListener(marker, 'dragend', function () {
        onMakerMove(marker);
    });
}

function codeAddress(address) {
    geocoder.geocode();

}


function getCantones(idProvincia) {
    var nombreC = $("#hdCantonID").val();
    var idCanton = $("#hddCantonID").val();
    $('#2').val($("#cantones option:selected").text());
    var l2 = $('#2').val();
    if (l2 == null || l2 == "") {
        $('#2').val($("#hdCantonID").val());
        var l2 = $('#2').val();
    }
   
    getData(
        "https://ubicaciones.paginasweb.cr/provincia/" + idProvincia + "/cantones.json",
        function (data) {
            arrayToOptions(data, $("#cantones"), 'Cantones', idCanton, nombreC);


        }
    );
}

function getDistritos(idCanton, idProvincia) {
    var idProvincia = $("#provincias").val();
    var idDistrito = $("#hddDistritoID").val();
    var idDistritos = $("#hdDistritoID").val();
    $('#3').val($("#distritos option:selected").text());
    var l3 = $('#3').val();
    if (l3 == "" || l3==null) {
        $('#3').val($("#hdDistritoID").val());
        var l3 = $('#3').val();
    }
    getData(
        "https://ubicaciones.paginasweb.cr/provincia/" + idProvincia + "/canton/" + idCanton + "/distritos.json",
        function (data) {
            arrayToOptions(data, $("#distritos"), 'Distritos', idDistrito, idDistritos );
        }
    );
}

function distritoSelected() {
    var query = "Costa Rica, " + jQuery('#provincias option:selected').text() + "," + jQuery('#cantones option:selected').text() + "," + jQuery('#distritos option:selected').text();
    codeAddress(query);

}

function arrayToOptions(array, $select, title, id,nombre) {
    $('.list-title span').html(title);
    if (id == null || id == "") {
        $select.html($('<option>').html('Seleccione la opción'));
    } else {
        $select.html($('<option>').html(nombre).val(id));
    }
 
    var $tableBody = $('.list tbody').html('');
    for (key in array) {
        $select.append($('<option>').html(array[key]).val(key));
        $tableBody.append($('<tr>').html($('<td>').html(key)).append($('<td>').html(array[key])));
    }
    
}



function getData(url, callback) {
    $.ajax({
        dataType: "json",
        url: url,
        success: function (data) {
            callback(data);
        },
        error: function (e) {
            console.log(e);
        }
    });
}

$(document).on('change', '#provincias', function (event) {
    $('#1').val($("#provincias option:selected").text());
    var l1 = $('#1').val();
});
$(document).on('change', '#cantones', function (event) {
    $('#2').val($("#cantones option:selected").text());
    var l2 = $('#2').val();
});
$(document).on('change', '#distritos', function (event) {
    $('#3').val($("#distritos option:selected").text());
    var l3 = $('#3').val();
});


function mostrar(id) {
    if (id == "1" || id == "2") {
        $("#CanBanos_C").show();
        $("#CanGarage_C").show();
        $("#CanCuartos_C").show();
        $("#MetrosCuadradosCasa_C").show();
    }
    if (id == "3") {
        $("#CanBanos_C").hide();
        $("#CanGarage_C").hide();
        $("#CanCuartos_C").hide();
        $("#MetrosCuadradosCasa_C").hide();
    }
  
        
    
}