$(document).ready(function () {

    $(".js-example-basic-single").select2();

    $(".js-example-basic-single").val('first').change();

    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    $("#sltNomImg").on('change', function () {
        VerImg(this.value);
    });

});



function AltaProducto() {
    if (ValidarCampos()) {
        var IdCategoria = $("#sltCategoriaM").val();
        var Nombre = $("#txtProductoM").val();
        var Modelo = $("#txtModeloM").val();
        var Descripcion = $("#txtDescripcionM").val();
        var Largo = $("#txtLargoM").val();
        var Ancho = $("#txtAnchoM").val();
        var Alto = $("#txtAltoM").val();
        var IdUMedidaT = $("#sltUMedidaTM").val();
        var Peso = $("#txtPesoM").val();
        var IdUMedidaP = $("#sltUMedidaPM").val();
        var CBarras = $("#txtCodBarrasM").val();
        var Precio = $("#txtPrecioM").val();


        resetToastPosition();
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
            url: '/Catalogos/Producto/Alta',
            type: "POST",
            data: {
                IdCategoria, Nombre, Modelo, Descripcion, Largo,
                Ancho, Alto, IdUMedidaT, Peso, IdUMedidaP, CBarras, Precio
            }
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
                $.toast({
                    heading: 'Guardado',
                    text: 'El registro se ha guardado correctamente',
                    showHideTransition: 'slide',
                    hideAfter: 6000,
                    icon: 'success',
                    loaderBg: '#f96868',
                    position: 'top-right'
                })
                LimpiarCampos();
                $("#AltaModal").modal("hide");
                $("#divListaProducto").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
                Ocultar();
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

function Buscar() {
    var Producto = $("#txtProducto").val();
    var Estatus = $("#sltEstatus").val();
    resetToastPosition();
    //var ToastInfo =
    $.toast({
        heading: 'Informaci&oacute;n',
        text: 'Buscando...',
        showHideTransition: 'slide',
        hideAfter: 3000,
        icon: 'info',
        loaderBg: '#46c35f',
        position: 'top-right'
    })
    $.ajax({
        url: '/Catalogos/Producto/Buscar',
        type: "POST",
        data: { Producto, Estatus }
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
            $.toast({
                heading: 'Busqueda',
                text: 'La busqueda se realizo correctamente',
                showHideTransition: 'slide',
                hideAfter: 5000,
                icon: 'success',
                loaderBg: '#f96868',
                position: 'top-right'
            })
            $("#divListaProducto").html(data.ListaCat);
            $('#tblProducto').DataTable({
                //responsive: false,
                searching: true,
                ordering: true,
                scrollX: true,
                scrollY: true,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ Productos",
                    "zeroRecords": "Ninguna Producto Encontrada - valide su busqueda",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay Productos disponible",
                    "infoFiltered": "(filtrado de _MAX_ Productos en total)",
                    "search": "Buscar:",
                    "paginate": {
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }
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
function LimpiarCamposBusqueda() {
    $("#txtProducto").val("");
    $("#sltEstatus").val('first').change();
}

function Editar(Id, IdCategoria, Nombre, Modelo, Descripcion, Largo, Ancho,
    Alto, IdUMedidaT, Peso, IdUMedidaP, CodBarras, PrecioVenta, EstatusSTR) {
    $("#txtIdProductoMEditar").val(Id);
    $("#sltCategoriaMEditar").val(IdCategoria).change();
    $("#txtProductoMEditar").val(Nombre);
    $("#txtModeloMEditar").val(Modelo);
    $("#txtDescripcionMEditar").val(Descripcion);
    $("#txtLargoMEditar").val(Largo);
    $("#txtAnchoMEditar").val(Ancho);
    $("#txtAltoMEditar").val(Alto);
    $("#sltUMedidaTMEditar").val(IdUMedidaT).change();
    $("#txtPesoMEditar").val(Peso);
    $("#sltUMedidaPMEditar").val(IdUMedidaP).change();
    $("#txtCodBarrasMEditar").val(CodBarras);
    $("#txtPrecioMEditar").val(PrecioVenta);
    if (EstatusSTR == "Activo") {
        $("#sltEstatusModificar").val(1).change();
    }
    else {
        $("#sltEstatusModificar").val(0).change();
    }

    MostrarMenu();
    $('#ModificarModal').modal('show');
    $("#txtProductoMEditar").focus();
}

function EditarProducto() {
    if (ValidarCamposEditar()) {
        var Id = parseInt($("#txtIdProductoMEditar").val());
        var IdCategoria = $("#sltCategoriaMEditar").val();
        var Nombre = $("#txtProductoMEditar").val();
        var Modelo = $("#txtModeloMEditar").val();
        var Descripcion = $("#txtDescripcionMEditar").val();
        var Largo = $("#txtLargoMEditar").val();
        var Ancho = $("#txtAnchoMEditar").val();
        var Alto = $("#txtAltoMEditar").val();
        var IdUMedidaT = $("#sltUMedidaTMEditar").val();
        var Peso = $("#txtPesoMEditar").val();
        var IdUMedidaP = $("#sltUMedidaPMEditar").val();
        var CodBarras = $("#txtCodBarrasMEditar").val();
        var PrecioVenta = $("#txtPrecioMEditar").val();
        var Estatus = $("#sltEstatusModificar").val() == "1" ? true : false;

        resetToastPosition();
        $.toast({
            heading: 'Informaci&oacute;n',
            text: 'Actualizando...',
            showHideTransition: 'slide',
            hideAfter: 3000,
            icon: 'info',
            loaderBg: '#46c35f',
            position: 'top-right'
        })
        $.ajax({
            url: '/Catalogos/Producto/Editar',
            type: "POST",
            data: {
                Id, IdCategoria, Nombre, Modelo, Descripcion, Largo, Ancho, Alto,
                IdUMedidaT, Peso, IdUMedidaP, CodBarras, PrecioVenta, Estatus
            }
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
                $.toast({
                    heading: 'Actualizado',
                    text: 'El registro se ha modificado correctamente',
                    showHideTransition: 'slide',
                    hideAfter: 6000,
                    icon: 'success',
                    loaderBg: '#f96868',
                    position: 'top-right'
                })
                LimpiarCamposEditar();
                $("#ModificarModal").modal("hide");
                $("#divListaProducto").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
                Ocultar();
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

function ValidarCamposEditar() {
    var Lista = new Array();
    var a = 0;
    var Final = true;

    var IdCategoria = $("#sltCategoriaMEditar").val();
    var Nombre = $("#txtProductoMEditar").val();
    var Modelo = $("#txtModeloMEditar").val();
    var Descripcion = $("#txtDescripcionMEditar").val();
    var Largo = $("#txtLargoMEditar").val();
    var Ancho = $("#txtAnchoMEditar").val();
    var Alto = $("#txtAltoMEditar").val();
    var IdUMedidaT = $("#sltUMedidaTMEditar").val();
    var Peso = $("#txtPesoMEditar").val();
    var IdUMedidaP = $("#sltUMedidaPMEditar").val();
    var CBarras = $("#txtCodBarrasMEditar").val();
    var PrecioVenta = $("#txtPrecioMEditar").val();
    var Estatus = $("#sltEstatusModificar").val() == "1" ? true : false;

    if (IdCategoria === "" || IdCategoria === null || IdCategoria === 0) {
        Lista[a] = "Categoria";
        a++;
    }
    if (Nombre === "") {
        Lista[a] = "Nombre";
        a++;
    }
    if (Modelo === "") {
        Lista[a] = "Modelo";
        a++;
    }
    if (Descripcion === "") {
        Lista[a] = "Descripcion";
        a++;
    }
    if (Largo === "" || Largo === 0 || Largo === 0.0) {
        Lista[a] = "Largo";
        a++;
    }
    if (Ancho === "" || Ancho === 0 || Ancho === 0.0) {
        Lista[a] = "Ancho";
        a++;
    }
    if (Alto === "" || Alto === 0 || Alto === 0.0) {
        Lista[a] = "Alto";
        a++;
    }
    if (IdUMedidaT === "" || IdUMedidaT === null || IdUMedidaT === 0) {
        Lista[a] = "Unidad Medida Tamaño";
        a++;
    }
    if (Peso === "" || Peso === 0) {
        Lista[a] = "Peso";
        a++;
    }
    if (IdUMedidaP === "" || IdUMedidaP === 0 || IdUMedidaP === null) {
        Lista[a] = "Unidad Medida Peso";
        a++;
    }
    if (CBarras === "") {
        Lista[a] = "Codigo de Barras";
        a++;
    }
    if (PrecioVenta === "" || PrecioVenta === 0) {
        Lista[a] = "Precio Venta";
        a++;
    }
    if (Estatus === "" || Estatus === null) {
        Lista[a] = "Estatus";
    }
    if (Lista.length > 0) {
        var Pendientes = " ";
        for (var i = 0; i < Lista.length; i++) {
            Pendientes = Pendientes + Lista[i];
            if (i === Lista.length - 1) {
                Pendientes = Pendientes;
            }
            else {
                Pendientes = Pendientes + ", ";
            }
        }
        swal({
            title: 'Informaci\u00f3n',
            text: 'Es obligatorio capturar: ' + Pendientes,
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
        Final = false;
    }
    else {

        Final = true;
    }

    return Final;
}

function LimpiarCamposEditar() {
    $("#sltCategoriaMEditar").val('first').change();
    $("#txtProductoMEditar").val("");
    $("#txtModeloMEditar").val("");
    $("#txtDescripcionMEditar").val("");
    $("#txtLargoMEditar").val("");
    $("#txtAnchoMEditar").val("");
    $("#txtAltoMEditar").val("");
    $("#sltUMedidaTMEditar").val('first').change();
    $("#txtPesoMEditar").val("");
    $("#sltUMedidaPMEditar").val('first').change();
    $("#txtCodBarrasMEditar").val("");
    $("#txtPrecioMEditar").val("");
    $("#sltEstatusModificar").val('first');
}

function Eliminar(Id, EstatusSTR) {
    if (ValidarEliminar(EstatusSTR)) {
        resetToastPosition();
        //var ToastInfo =
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
            url: '/Catalogos/Producto/Eliminar',
            type: "POST",
            data: { Id }
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
                $.toast({
                    heading: 'Eliminado',
                    text: 'El registro se ha Eliminado correctamente',
                    showHideTransition: 'slide',
                    hideAfter: 6000,
                    icon: 'success',
                    loaderBg: '#f96868',
                    position: 'top-right'
                })
                $("#divListaProducto").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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


//Funcion para validar si el registro ya tiene estatus 0 ya no se trate de eliminar e informar al usuario
function ValidarEliminar(EstatusSTR) {
    var Valido = true;

    if (EstatusSTR === "Activo") {
        Valido = true;
    }
    else {
        Valido = false;
    }
    if (!Valido) {
        swal({
            title: 'Informaci\u00f3n',
            text: 'El registro ya tiene estatus "Inactivo"',
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

function AgregarImagen(Id, Nombre) {
    resetToastPosition();
    //var ToastInfo =
    $.toast({
        heading: 'Informaci&oacute;n',
        text: 'Consultando...',
        showHideTransition: 'slide',
        hideAfter: 3000,
        icon: 'info',
        loaderBg: '#46c35f',
        position: 'top-right'
    })
    $.ajax({
        url: '/Catalogos/Producto/ObtenerImgs',
        type: "POST",
        data: { Id }
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
            $.toast({
                heading: 'Informacion',
                text: 'Consultado',
                showHideTransition: 'slide',
                hideAfter: 6000,
                icon: 'success',
                loaderBg: '#f96868',
                position: 'top-right'
            })

            $("#tbodytblImgProd").empty();//.remove();//.html("");
            $.each(data.limgs, function (Id, Dato) {
                $("#tblImgProd").find('tbody').append('<tr>' +
                    '<td class="pl-0">' + Dato.Id + '</td>' +
                    '<td class="pr-0 text-center">' + Dato.Dato + '</td>' +
                    '<td class="pr-0 text-center"><div class="badge badge-pill badge-primary text-center">' +
                    '<i class="mdi mdi-pencil" onclick="EditRow(\'' + Dato.Id + '\',' + Dato.Dato + ', this)" style="cursor:pointer" title="Editar">' +
                    '</i></div>' +
                    '<div class="badge badge-pill badge-warning text-center">' +
                    '<i class="mdi mdi-trash-can" onclick="deleteRow(this, \'' + Dato.Id + '\' )"style="cursor:pointer" title ="Eliminar"></i></div>' +
                    '</td >' +
                '</tr > ');
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

    $("#AltaImgModalLabel").text(Nombre);
    $("#txtIdProductoMD").val(Id);
    MostrarMenu();
    $("#ImgModal").modal("show");
}

function AltaMProducto() {
    if (ValidarDocumentos()) {
        var IdProducto = $("#txtIdProductoMD").val();
        var IdRuta = $("#txtIdRutaMD").val();
        var Ruta = $("#txtRutaMD").val();

        resetToastPosition();
        //var ToastInfo =
        $.toast({
            heading: 'Informaci&oacute;n',
            text: 'Procesando...',
            showHideTransition: 'slide',
            hideAfter: 3000,
            icon: 'info',
            loaderBg: '#46c35f',
            position: 'top-right'
        })
        $.ajax({
            url: '/Catalogos/Producto/CrearCarpeta',
            type: "POST",
            data: { IdProducto, IdRuta, Ruta }
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
                $.toast({
                    heading: 'Informaci&oacute;n',
                    text: 'Almacenando...',
                    showHideTransition: 'slide',
                    hideAfter: 3000,
                    icon: 'info',
                    loaderBg: '#46c35f',
                    position: 'top-right'
                })
                var Archivos = $("#iptMedia").get(0).files;
                var ArchivosData = new FormData();
                for (var i = 0; i < Archivos.length; i++) {
                    ArchivosData.append("iptMedia", Archivos[i]);
                }
                $.ajax({
                    url: "/Catalogos/Producto/AgregarArchivo",
                    type: "POST",
                    autoUpload: true,
                    contentType: false,
                    processData: false,
                    data: ArchivosData
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
                        $('#sltNomImg').html("");
                        $('#sltNomImg').append($('<option>', {
                            value: 0,
                            text: 'Seleccione Imagen'
                        }));                        
                        $.each(data.lstFile, function (i, item) {
                            $('#sltNomImg').append($('<option>', {
                                value: item,
                                text: item
                            }));
                        });
                        $('#sltOrdenImg').html("");
                        $('#sltOrdenImg').append($('<option>', {
                            value: 0,
                            text: 'Seleccione Consecutivo'
                        }));
                        var uno = data.primero;
                        for (uno; uno <= data.final; uno++) {
                            $('#sltOrdenImg').append($('<option>', {
                                value: uno,
                                text: uno
                            }));
                        }
                        LimpiarMedia();
                        $.toast({
                            heading: 'Informaci&oacute;n',
                            text: 'Procesado',
                            showHideTransition: 'slide',
                            hideAfter: 3000,
                            icon: 'info',
                            loaderBg: '#46c35f',
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

function ValidarDocumentos() {
    var IdProd = parseInt($("#txtIdProductoMD").val());
    var Archivos = $("#iptMedia").get(0).files;
    var Final = true;
    var Documento = true;
    var Archivo = true;
    if (IdProd === 0 || IdProd === null) {
        Documento = false;
    }
    if (Archivos.length === 0) {
        Archivo = false;
    }
    if (Documento === false || Archivo === false) {
        swal({
            title: 'Informaci\u00f3n',
            text: 'Falta capturar informaci\u00f3n',
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
        Final = false;
    }
    return Final;
}

function LimpiarMedia() {
    $('#iptMedia').parent().find(".dropify-clear").trigger('click');

}

function VerImg(Value) {
    var IdProd = $("#txtIdProductoMD").val();
    var Ubi = $("#txtRutaMD").val();

    var UbiC = Ubi + IdProd + "/" + Value;
    //Debo agregar la ruta virtual y combinarlas con eso podre ver las imagenes solo eso falta
    $("#divViewImg").html("");
    $("#divViewImg").append('<img src="' + UbiC + '"' + 'width="auto" height="auto" alt="Image"' +'/>');//('<img src="C:/Mercar/Producto/1/ImgV-1-2.jpg" alt="Image"/>');                   
    
}

function AgregarOrden() {
    if (ValidarOrden()) {
        var Img = $("#sltNomImg").val();
        var Cons = $("#sltOrdenImg").val();

        $("#tblImgProd").find('tbody').append('<tr>' +
            '<td class="pl-0">' + Img + '</td>' +
            '<td class="pr-0 text-center">' + Cons + '</td>' +
            '<td class="pr-0 text-center"><div class="badge badge-pill badge-primary text-center">' +
            '<i class="mdi mdi-pencil" onclick="EditRow(\'' + Img + '\',' + Cons + ', this); " style="cursor: pointer" title ="Editar">' +
            '</i></div>' +
            '<div class="badge badge-pill badge-warning text-center">' +
            '<i class="mdi mdi-trash-can" onclick="deleteRow(this, \'' + Img + '\')"style="cursor:pointer" title ="Eliminar"></i></div>' +
            '</td>' +
            '</tr>');

        var x = document.getElementById("sltNomImg");
        x.remove(x.selectedIndex);

        var y = document.getElementById("sltOrdenImg");
        y.remove(y.selectedIndex);
    }
}

function ValidarOrden() {
   
    var Final = true;
    var Img = $("#sltNomImg").val();
    var Ord = $("#sltOrdenImg").val();

    if (Img === null || Img === 0 || Img === "0") {
        Final = false;
    }
    if (Ord === null || Ord === 0 || Ord === "0") {
        Final = false;
    }
    if (Final === false) {
        swal({
            title: 'Informaci\u00f3n',
            text: 'Falta capturar informaci\u00f3n',
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
    return Final;
}

function LimpiarCamposOrden() {
    $("#sltNomImg").val('first').change();
    $("#sltOrdenImg").val('first').change();

}

function EditRow(Id, Dato, btn) {

    $('#sltNomImg').append($('<option>', {
        value: Id,
        text: Id
    }));
    
    $("#sltNomImg").val(Id).change();
    VerImg(Id);

    $('#sltOrdenImg').append($('<option>', {
        value: Dato,
        text: Dato
    })); 

    var row = btn.parentNode.parentNode.parentNode;    
    row.parentNode.removeChild(row);
}

function deleteRow(btn, Value) {
    var IdProd = $("#txtIdProductoMD").val();
    var Ubi = $("#txtRutaMD").val();
    var UbiC = Ubi + "\\" + IdProd + "\\" + Value;

    resetToastPosition();
    //var ToastInfo =
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
        url: '/Catalogos/Producto/EliminarMedia',
        type: "POST",
        data: { UbiC }
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
            $.toast({
                heading: 'Informaci&oacute;n',
                text: 'Eliminado...',
                showHideTransition: 'slide',
                hideAfter: 3000,
                icon: 'info',
                loaderBg: '#46c35f',
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

    var row = btn.parentNode.parentNode.parentNode;
    
       row.parentNode.removeChild(row);
}

function GuardarImgTbl() {
    
    var ComboAnonymous = new Array();
    $("#tblImgProd TBODY TR").each(function () {
        var row = $(this);
        var Combo = {};
        Combo.Id = row.find("TD").eq(0).html();
        Combo.Dato = row.find("TD").eq(1).html();
        ComboAnonymous.push(Combo);
    });

    $.ajax({
        url: "/Catalogos/Producto/GuardarTabla",
        type: "POST",
        data: { ComboAnonymous }
        
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
            $.toast({
                heading: 'Informaci&oacute;n',
                text: 'Procesado',
                showHideTransition: 'slide',
                hideAfter: 3000,
                icon: 'info',
                loaderBg: '#46c35f',
                position: 'top-right'
            })
            LimpiarImgMdl();
            $("#ImgModal").modal("hide");
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

//Funcion que limpia modal donde se cargan y muestran las imagenes así como la tabla donde se organizan
function LimpiarImgMdl() {
    LimpiarMedia();
    $("#divViewImg").html("");
    $('#sltNomImg').html("");
    $('#sltOrdenImg').html("");
    $("#tbodytblImgProd").empty();
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