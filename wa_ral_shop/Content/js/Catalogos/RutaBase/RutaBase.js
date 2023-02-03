function AltaRutaBase() {
    if (ValidarCampos()) {
        var RutaBase = $("#txtRutaBaseM").val();
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
            url: '/Catalogos/RutaBase/Alta',
            type: "POST",
            data: { RutaBase }
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
                $("#divListaRutaBase").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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
    var dato = $("#txtRutaBaseM").val();
    if (dato === "") {
        Valido = false;
    }
    if (!Valido) {
        swal({
            title: 'Informaci\u00f3n',
            text: 'Es obligatorio capturar RutaBase',
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

function LimpiarCampos() {
    $("#txtRutaBaseM").val("");
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
    var RutaBase = $("#txtRutaBase").val();
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
        url: '/Catalogos/RutaBase/Buscar',
        type: "POST",
        data: { RutaBase, Estatus }
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
            $("#divListaRutaBase").html(data.ListaCat);
            $('#tblRutaBase').DataTable({
                //responsive: false,
                searching: true,
                ordering: true,
                //scrollX: true,
                scrollY: true,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ Rutas Base",
                    "zeroRecords": "Ninguna Ruta Base Encontrada - valide su busqueda",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay Rutas Base disponible",
                    "infoFiltered": "(filtrado de _MAX_ Rutas Base en total)",
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
    $("#txtRutaBase").val("");
    $("#sltEstatus").val('first').change();
}

function Editar(Id, RutaBase, Estatus) {
    $("#txtIdRutaBaseMEditar").val(Id);
    $("#txtRutaBaseMEditar").val(RutaBase);
    if (Estatus == "Activo") {
        $("#sltEstatusModificar").val(1).change();
    }
    else {
        $("#sltEstatusModificar").val(0).change();
    }

    MostrarMenu();
    $('#ModificarModal').modal('show');
    $("#txtRutaBaseMEditar").focus();
}

function EditarRutaBase() {
    if (ValidarCamposEditar()) {
        var Id = parseInt($("#txtIdRutaBaseMEditar").val());
        var RutaBase = $("#txtRutaBaseMEditar").val();
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
            url: '/Catalogos/RutaBase/Editar',
            type: "POST",
            data: { Id, RutaBase, Estatus }
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
                $("#divListaRutaBase").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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
    var Valido = true;
    var dato = $("#txtRutaBaseMEditar").val();
    if (dato === "") {
        Valido = false;
    }
    if (!Valido) {
        swal({
            title: 'Informaci\u00f3n',
            text: 'Es obligatorio capturar Ruta Base',
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

function LimpiarCamposEditar() {
    $("#txtRutaBaseMEditar").val("");
    //sltEstatus
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
            url: '/Catalogos/RutaBase/Eliminar',
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
                $("#divListaRutaBase").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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


//Funcion que valida si se ha de ocultar o mostrar el menu izquierdo
function Ocultar() {
    var body = $('body');
    if ((body.hasClass('sidebar-toggle-display')) || (body.hasClass('sidebar-absolute'))) {
        body.toggleClass('sidebar-hidden', false);
    } //else {
    //    body.toggleClass('sidebar-toggle-display');
    //}
}

function MostrarMenu() {
    var body1 = $('body');
    if ((body1.hasClass('sidebar-toggle-display')) || (body1.hasClass('sidebar-absolute'))) {
        body1.toggleClass('sidebar-hidden', true);
    } else {
        body1.toggleClass('sidebar-toggle-display');
    }
}