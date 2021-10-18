$(document).ready(function () {
    getData(
        "https://ubicaciones.paginasweb.cr/provincias.json",
        function (data) {
            arrayToOptions(data, $("#provincias"), 'Provincias');
            $("#Dolares").hide();
            $("#Colones").hide();
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
    codeAddress("Costa Rica, " + $('#provincias option:selected').text());
    getData(
        "https://ubicaciones.paginasweb.cr/provincia/" + idProvincia + "/cantones.json",
        function (data) {
            arrayToOptions(data, $("#cantones"), 'Cantones');
            $(".canton").show();
            $(".distrito").hide();

        }
    );
}

function getDistritos(idCanton) {
    var query = "Costa Rica, " + $('#provincias option:selected').text() + ', ' + $('#cantones option:selected').text();
    codeAddress(query);
    var idProvincia = $("#provincias").val();
    getData(
        "https://ubicaciones.paginasweb.cr/provincia/" + idProvincia + "/canton/" + idCanton + "/distritos.json",
        function (data) {
            arrayToOptions(data, $("#distritos"), 'Distritos');
            $(".distrito").show();

        }
    );
}

function distritoSelected() {
    var query = "Costa Rica, " + jQuery('#provincias option:selected').text() + "," + jQuery('#cantones option:selected').text() + "," + jQuery('#distritos option:selected').text();
    codeAddress(query);

}

function arrayToOptions(array, $select, title) {
    $('.list-title span').html(title);
    $select.html($('<option>').html('Seleccione una opción'));
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
});
$(document).on('change', '#cantones', function (event) {
    $('#2').val($("#cantones option:selected").text());
});
$(document).on('change', '#distritos', function (event) {
    $('#3').val($("#distritos option:selected").text());
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

function moneda(id) {
    if (id == "¢") {
        $("#Colones").show();
        $("#Dolares").hide();
    }
    if (id == "$") {
        $("#Colones").hide();
        $("#Dolares").show();
    }




}