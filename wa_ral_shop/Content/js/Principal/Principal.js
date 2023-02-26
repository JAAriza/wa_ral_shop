$(document).ready(function () {
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
                var Pv = new Array();
                Pv = Dato.PrecioVenta.toString().split('.');
                if (Pv.length == 1) {
                    Pv.push("00");
                }
                $("#divDashboard").append(
                    '<div class="card">' +
                    '<div class="card-header">' +
                    '<div class="nonloop owl-carousel owl-theme full-width" id="div-' + Dato.Id + '">' +
                    '</div>' +
                    '</div>' +
                    '<div>' +
                    '<button style="border:none; background:none; float:left" class="text-md-right" title="Favorito" Id="Favorito-' + Dato.Id + '" onClick="AgregarFavorito(' + Dato.Id + ')">' +
                    '<i Id="iFavorito-' + Dato.Id + '" style="color:#dcdcdc" class="mdi mdi-heart mdi-24px"></i>' +
                    '</button>' +
                    '<button style="border:none; background:none; float:right" class="text-md-right" title="Carrito" Id ="Carrito-' + Dato.Id + '" onClick="AgregarCarrito(' + Dato.Id + ')">' +
                    '<i Id="iCarrito-' + Dato.Id+'" style = "color:#dcdcdc" class= "fa fa-shopping-cart fa-2x" ></i > ' +
                    '</button>' +
                    '</div>' +
                    '<div class="card-body">' +
                    '<h4 class="card-title">' + Dato.Nombre + '</h4>' +

                    '<s style="color:#dcdcdc"><span style="color:#dcdcdc;">$' + Dato.PrecioVenta + 15 + '</span></s>' +
                    '<br />' +
                    '<span style="font-size:large">$' + Pv[0] + '<sup>' + Pv[1] + '</sup>&nbsp; </span>' +
                    '<span style="color:#ff914d;">12% OFF</span>' +
                    //'<br />' +
                    //'<span style="color:#dcdcdc; float:right">Existencias: ' + Dato.Existencias + '</span>' +
                    '</div>' +
                    '</div>');
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

function AgregarFavorito(IdProducto) {
    var f = "iFavorito-" + IdProducto;
    if ($('#' + f).css('color') == 'rgb(255, 0, 0)') {

        $.toast({
            heading: 'Informaci&oacute;n',
            text: 'Eliminando...',
            showHideTransition: 'slide',
            hideAfter: 3000,
            icon: 'info',
            loaderBg: '#46c35f',
            position: 'top-right'
        })
        $.ajax({
            url: '/Catalogos/Favorito/Eliminar',
            type: "POST",
            data: { IdProducto }

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
                $('#' + f).css('color', '#dcdcdc');
                $.toast({
                    heading: 'Eliminado',
                    text: 'El registro se ha eliminado correctamente',
                    showHideTransition: 'slide',
                    hideAfter: 6000,
                    icon: 'success',
                    loaderBg: '#f96868',
                    position: 'top-right'
                })
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
    else {
        //resetToastPosition();
        //var ToastInfo =
        $.toast({
            heading: 'Informaci&oacute;n',
            text: 'Guardando...',
            showHideTransition: 'slide',
            hideAfter: 3000,
            icon: 'info',
            loaderBg: '#46c35f',
            position: 'top-right'
        })
        $.ajax({
            url: '/Catalogos/Favorito/Alta',
            type: "POST",
            data: { IdProducto }

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
                $('#' + f).css('color', '#FF0000');

                $.toast({
                    heading: 'Guardado',
                    text: 'El registro se ha guardado correctamente',
                    showHideTransition: 'slide',
                    hideAfter: 6000,
                    icon: 'success',
                    loaderBg: '#f96868',
                    position: 'top-right'
                })
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
}

function AgregarCarrito(IdProducto) {
    var f = "iCarrito-" + IdProducto;
    if ($('#' + f).css('color') == 'rgb(255, 255, 0)') {

        $.toast({
            heading: 'Informaci&oacute;n',
            text: 'Eliminando...',
            showHideTransition: 'slide',
            hideAfter: 3000,
            icon: 'info',
            loaderBg: '#46c35f',
            position: 'top-right'
        })
        $.ajax({
            url: '/Catalogos/Carrito/Eliminar',
            type: "POST",
            data: { IdProducto }

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
                $('#' + f).css('color', '#dcdcdc');
                $.toast({
                    heading: 'Eliminado',
                    text: 'El registro se ha eliminado correctamente',
                    showHideTransition: 'slide',
                    hideAfter: 6000,
                    icon: 'success',
                    loaderBg: '#f96868',
                    position: 'top-right'
                })
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
    else {
        //resetToastPosition();
        //var ToastInfo =
        $.toast({
            heading: 'Informaci&oacute;n',
            text: 'Guardando...',
            showHideTransition: 'slide',
            hideAfter: 3000,
            icon: 'info',
            loaderBg: '#46c35f',
            position: 'top-right'
        })
        $.ajax({
            url: '/Catalogos/Carrito/Agregar',
            type: "POST",
            data: { IdProducto }

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
                $('#' + f).css('color', '#FFFF00');

                $.toast({
                    heading: 'Guardado',
                    text: 'El registro se ha guardado correctamente',
                    showHideTransition: 'slide',
                    hideAfter: 6000,
                    icon: 'success',
                    loaderBg: '#f96868',
                    position: 'top-right'
                })
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