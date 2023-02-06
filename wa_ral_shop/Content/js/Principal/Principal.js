﻿$(document).ready(function () {
    ConsultaProductosActivos();
});

function ConsultaProductosActivos() {
    resetToastPosition();
    //var ToastInfo =
    //$.toast({
    //    heading: 'Informaci&oacute;n',
    //    text: 'Guardando...',
    //    showHideTransition: 'slide',
    //    hideAfter: 3000,
    //    icon: 'info',
    //    loaderBg: '#46c35f',
    //    position: 'top-right'
    //})
    $.ajax({
        url: '/Catalogos/Producto/ConsultarProductosActivos',
        type: "POST"

    }).done(function (data, textStatus, jqXHR) {
        if (data.codigo === "Error") {
            resetToastPosition();
            $.toast({
                heading: 'Error',
                text: data.mensaje,
                showHideTransition: 'slide',
                hideAfter: 8000,
                icon: 'error',
                loaderBg: '#f2a654',
                position: 'top-right'
            })
        }
        else {
            //resetToastPosition();
            //Revisar bien el codigo para que itere el numero de veces necesario para mostrar
            //las imagenes que son por producto asi como los precios igual creo que hace falta
            //nombrar mas div para tener mejor control pero eso ya lo voy solucionando con las pruebas
            $("#divDashboard").html("");
            $.each(data.LProd, function (Id, Dato) {
                $("#divDashboard").append(
                    '<div class="card">' +
                    '<div class="card-header">' +
                    '<div class="nonloop owl-carousel owl-theme full-width" id="div-' + Dato.Id + '">' +
                    '</div>' +
                    '</div>' +
                    '<div>' +
                    '<button style="border:none; background:none; float:left" class="text-md-right" title="Favorito" Id="Favorito-' + Dato.Id + '" onClick="AgregarFavorito(' + Dato.Id + ')">' +
                    '<i style="color:#dcdcdc" class="mdi mdi-heart mdi-24px"></i>' +
                    '</button>' +
                    '<button style="border:none; background:none; float:right" class="text-md-right" title="Carrito" Id ="Carrito-' + Dato.Id + '" onClick="AgregarCarrito(' + Dato.Id + ')">' +
                    '<i style="color:#dcdcdc" class="fa fa-shopping-cart fa-2x"></i>' +
                    '</button>' +
                    '</div>' +
                    '<div class="card-body">' +
                    '<h4 class="card-title">' + Dato.Nombre + '</h4>' +

                    '<s style="color:#dcdcdc"><span style="color:#dcdcdc;">$' + Dato.PrecioVenta + 15 + '</span></s>' +
                    '<br />' +
                    '<span style="font-size:large">$' + Dato.PrecioVenta + '<sup>00</sup>&nbsp; </span><span style="color:#ff914d;">12% OFF</span>' +
                    '</div>' +
                    '</div >');
            });
            $.each(data.LProd, function (Id, Dato) {
                $.each(data.LPI, function (Id2, Dato2) {
                    if (Dato.Id == Dato2.IdProducto) {
                        $("#div-" + Dato.Id + "").append(
                            '<div class="item">' +
                            '<img src="' + data.RutaImg + '\\' + Dato2.IdProducto + '\\' + Dato2.Nombre + '" alt="image"/>' +
                            //'<img src="/Content/images/carousel/banner_12.jpg" alt="image">'+
                            '</div>'
                        );
                    }
                }
                );
            });
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        resetToastPosition();
        $.toast({
            heading: 'Error',
            text: errorThrown,
            showHideTransition: 'slide',
            hideAfter: 8000,
            icon: 'error',
            loaderBg: '#f2a654',
            position: 'top-right'
        })
    }).always(function (data, textStatus, errorThrown) {
        // ToastInfo.reset();
    });
}

function ValidarCampos() {
    var Valido = true;
    var dato = $("#txtProductoM").val();
    if (dato === "") {
        Valido = false;
    }
    if (!Valido) {
        swal({
            title: 'Informaci\u00f3n',
            text: 'Es obligatorio capturar Producto',
            icon: 'warning',
            showCancelButton: false,
            button: {
                text: "Aceptar",
                value: true,
                visible: true,
                className: "btn btn-primary",
                closeModal: true
            }
        });
    }
    return Valido;
}

resetToastPosition = function () {
    $('.jq-toast-wrap').removeClass('bottom-left bottom-right top-left top-right mid-center'); // to remove previous position class
    $(".jq-toast-wrap").css({
        "top": "",
        "left": "",
        "bottom": "",
        "right": ""
    }); //to remove previous position style
}

//Funcion que valida si se ha de mostrar el menu izquierdo
function Ocultar() {
    var body = $('body');
    if ((body.hasClass('sidebar-toggle-display')) || (body.hasClass('sidebar-absolute'))) {
        body.toggleClass('sidebar-hidden', false);
    } //else {
    //    body.toggleClass('sidebar-toggle-display');
    //}
}

//Funcion que valida si se ha de ocultar el menu izquierdo
function MostrarMenu() {
    var body1 = $('body');
    if ((body1.hasClass('sidebar-toggle-display')) || (body1.hasClass('sidebar-absolute'))) {
        body1.toggleClass('sidebar-hidden', true);
    } else {
        body1.toggleClass('sidebar-toggle-display');
    }
}