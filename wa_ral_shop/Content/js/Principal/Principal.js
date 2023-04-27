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
                    '<i Id="iCarrito-' + Dato.Id + '" style = "color:#dcdcdc" class= "fa fa-shopping-cart fa-2x" ></i > ' +
                    '</button>' +
                    '</div>' +
                    '<div class="card-body">' +
                    '<h4 class="card-title" onclick="ConsultaProductoDetalle('+Dato.Id+');" style="cursor:pointer;">' + Dato.Nombre + '</h4>' +
                    
                    
                    '<s style="color:#dcdcdc"><span style="color:#dcdcdc;" class="currency">$' + parseFloat(parseFloat(Dato.PrecioVenta) * parseFloat(1.12)).toFixed(2) + '</span></s>' +
                    '<br />' +
                    '<span style="font-size:large">$' + Pv[0] + '<sup>' + Pv[1] + '</sup>&nbsp; </span>' +
                    '<span style="color:#ff914d;">12% OFF</span>' +
                    '<br />' +
                    '<br />' +
                    '<div class="form-group row">' +
                    '<div clas="col-sm-2"' +
                    '<span style="color:#dcdcdc; float:right">Existencias: ' + Dato.Existencias + '</span>' +
                    '</div>'+
                    '<div class="col-sm-2">' +
                    '<label></label>' +
                    '<button type="button" class="btn btn-secondary" style="border:none; background:none; float:left;" id="btnMenos-' + Dato.Id + '" onclick="Menos(' + Dato.Id + ' , 10)">' + //+ Dato.Existencias + ')">' +
                    '<i class="fa fa-minus" style:"cursor:pointer;"></i>' +
                    '</button>' +
                    '</div>' +
                    '<div class="col-sm-3">' +
                    '<label>Cantidad:</label>' +
                    //'<input class="form-control input-number" min="1" max="' + Dato.Existencias + '" value="' + Dato.Cantidad + '" id="txtCantidad-' + Dato.Id + '" type="number" step="1">' +
                    '<input class="form-control input-number" min="1" max="10" value="1" id="txtCantidad-' + Dato.Id + '" type="number" step="1" onchange="ValidarExistencias(this.value, ' + Dato.Id + ' , 10)">' + //' + Dato.Existencias + ' )">' +
                    '</div>' +
                    '<div class="col-sm-2">' +
                    '<label></label>' +
                    '<button type="button" class="btn btn-secondary" style="border:none; background:none; float:right;" id="btnMas-' + Dato.Id + '" onclick="Mas(' + Dato.Id + ' , 10)">' +
                    '<i class="fa fa-plus" style:"cursor:pointer;"></i>' +
                    '</button>' +
                    '</div>' +
                    '</div>' +

                    '</div>' +
                    '</div>');
            });
            $.each(data.LProd, function (Id, Dato) {
                $.each(data.LPI, function (Id2, Dato2) {
                    if (Dato.Id == Dato2.IdProducto) {
                        $("#div-" + Dato.Id + "").append(
                            '<div class="item">' +
                            //'<img src="' + data.RutaImg + '\\' + Dato2.IdProducto + '\\' + Dato2.Nombre + '" alt="image"/>' +
                            '<img src="/../Content/images/carousel/banner_1.jpg" alt="image"/>' +
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

function Menos(Id, Existencias) {
    var f = "txtCantidad-" + Id;
    var Cantidad = $('#' + f).val();
    if (Cantidad == 1) {
        $('#' + f).val(1);
    }
    else if (Cantidad == 0) {
        $('#' + f).val(1);
    }
    else {
        $('#' + f).val(Cantidad - 1);
    }
    if (Cantidad > Existencias) {
        $('#' + f).val(Existencias);
    }
}

function ValidarExistencias(a, Id, Existencias) {
    var f = "txtCantidad-" + Id;

    if (a > Existencias) {
        $('#' + f).val(Existencias);
    }

}

function Mas(Id, Existencias) {
    var f = "txtCantidad-" + Id;
    var Cantidad = $('#' + f).val();
    if (Cantidad == 1) {
        $('#' + f).val(parseInt(Cantidad) + parseInt(1));
    }
    else if (Cantidad == 0) {
        $('#' + f).val(1);
    }
    else {
        if ((parseInt(Cantidad) + parseInt(1)) > Existencias) {
            $('#' + f).val(Existencias);
        }
        else {
            $('#' + f).val(parseInt(Cantidad) + parseInt(1));
        }

    }
    if (Cantidad > Existencias) {
        $('#' + f).val(Existencias);
    }

}


function ConsultaProductoDetalle(IdProducto) {
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
        url: '/Catalogos/Producto/ConsultarProductoDetalle',
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

            $("#divBodyDet").html("");
            $.each(data.LProd, function (Id, Dato) {
                var Pv = new Array();
                Pv = Dato.PrecioVenta.toString().split('.');
                if (Pv.length == 1) {
                    Pv.push("00");
                }
                $("#DetProdLabel").text(Dato.Nombre);
                $("#divBodyDet").append(
                    '<br />' +
                    '<span><b>Modelo: </b>' + Dato.Modelo + '</span>' +
                    '<br />' +
                    '<br />' +
                    '<p>' + Dato.Descripcion + '</p>' +
                    '<br />' +
                    '<span><b>Tamaño: </b>' + Dato.Largo + '*' + Dato.Ancho + '*' + Dato.Alto + '&nbsp;' + Dato.unidadMedidaT.Nombre + '</span>' +
                    '<br />' +
                    '<span><b>Peso: </b>' + Dato.Peso + '&nbsp;' + Dato.unidadMedidaP.Nombre + '</span>' +
                    '<br />' +
                    '<s style="color:#dcdcdc"><span style="color:#dcdcdc;">$' + parseFloat(Dato.PrecioVenta) + parseFloat(15) + '</span></s>' +
                    '<br />' +
                    '<span style="font-size:large">$' + Pv[0] + '<sup>' + Pv[1] + '</sup>&nbsp; </span>' +
                    '<span style="color:#ff914d;">12% OFF</span>' +
                    '<br />' +
                    '<span style="color:#dcdcdc; float:right">Existencias: ' + Dato.Existencias + '</span>'
                );
            });
            
            $("#divBodyImg").html("");
            $.each(data.LPI, function (Id2, Dato2) {
                if (Id2 == 0) {
                    $("#divBodyImg").append(
                        '<div class=" mb-2">' +
                        //'< img src="' + data.RutaImg + '\\' + Dato2.Nombre + '" class="big_image mt-2 border border-0 w-100 " alt="img" style="width:auto; height:auto;" >' +
                        '<img src="https://localhost:44301/Content/images/carousel/banner_1.jpg" class="big_image mt-2 border border-0 w-100 " alt="img" style="width:auto; height:auto;">' +
                        '</div >' +
                        '<div class="d-flex" style="overflow-x:auto;" id="divImg">' +
                        '</div>'
                    );
                }
            }
            );
              
            $("#divImg").html("");
            $.each(data.LPI, function (Id2, Dato2) {
                $("#divImg").append(                    
                    //'<img src="' + data.RutaImg + '\\' + Dato2.Nombre + '" class="small_img border border-0 w-25" >'                    
                    '<img src="https://localhost:44301/Content/images/carousel/banner_1.jpg" class="small_img border border-0 w-25">'
                );
            }
            );
            MostrarModal();
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

function MostrarModal() {
    MostrarMenu();
    $('#ModalDetProd').modal('show');

    //Codigo para funcionamiento de zoom
    $(".small_img").hover(function () {
        $(".big_image").attr('src', $(this).attr('src'))
    });
    $(".big_image").imagezoomsl(function () {
        zoomrange: [4, 4]
    });
    $('#modelId').on('shown.bs.modal', function () {
        $(".big_image").imagezoomsl(function () {
            zoomrange: [4, 4]
        });
    })
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

    var t = "txtCantidad-" + IdProducto;
    var Cantidad = $('#' + t).val();

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
            url: '/Catalogos/Carrito/Alta',
            type: "POST",
            data: { IdProducto, Cantidad }

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