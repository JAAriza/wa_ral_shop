$(document).ready(function () {

    $(".js-example-basic-single").select2();

    $(".js-example-basic-single").val('first').change();
});

function AltaColaborador() {
    if (ValidarCampos()) {
        var IdPuesto = $("#sltPuestoM").val();
        var Nombre = $("#txtNombreM").val();
        var APaterno = $("#txtAPaternoM").val();
        var AMaterno = $("#txtAMaternoM").val();
        var Telefono = $("#txtTelefonoM").val();
        var EMail = $("#txtEMailM").val();
        var FHCon = $("#txtFecConM").val();

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
            url: '/Catalogos/Colaborador/Alta',
            type: "POST",
            data: { IdPuesto, Nombre, APaterno, AMaterno, Telefono, EMail, FHCon  }
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
                $("#divListaColaborador").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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
    var Lista = new Array();
    var a = 0;

    var Final = true;

    var IdPuesto = $("#sltPuestoM").val();
    var Nombre = $("#txtNombreM").val();
    var APaterno = $("#txtAPaternoM").val();
    var AMaterno = $("#txtAMaternoM").val();
    var Telefono = $("#txtTelefonoM").val();
    var EMail = $("#txtEMailM").val();
    var FhCon = $("#txtFecConM").val();

    if (IdPuesto === "" || IdPuesto === 0 || IdPuesto === null) {
        Lista[a] = "Puesto";
        a++;
    }
    if (Nombre === "") {
        Lista[a] = "Nombre";
        a++;
    }
    if (APaterno === "") {
        Lista[a] = "Apellido Paterno";
        a++;
    }
    if (AMaterno === "") {
        Lista[a] = "Apellido Materno";
        a++;
    }
    if (Telefono === "") {
        Lista[a] = "Telefono";
        a++;
    }
    if (EMail === "") {
        Lista[a] = "EMail";
        a++;
    }
    if (FhCon === "") {
        Lista[a] = "FechaContratacion";
    }
    if (Lista.length > 0) {
        var Pendientes = " ";
        for (var i = 0; i < Lista.length; i++) {
            Pendientes = Pendientes + Lista[i];
            if (i === Lista.length-1) {
                Pendientes = Pendientes;
            }
            else {
                Pendientes = Pendientes + ", ";
            }
        }

        swal({
            title: 'Informaci\u00f3n',
            text: 'Es obligatorio capturar:' + Pendientes,
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

function LimpiarCampos() {
    $("#sltPuestoM").val('first').change();
    $("#txtNombreM").val("");
    $("#txtAPaternoM").val("");
    $("#txtAMaternoM").val("");
    $("#txtTelefonoM").val("");
    $("#txtEMailM").val("");
    $("#txtFecConM").val();
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
    var Nombre = $("#txtNombre").val();
    var IdPuesto = $("#sltPuesto").val();
    var Estatus = $("#sltEstatus").val();
    if (IdPuesto === null) {
        IdPuesto = 0;
    }
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
        url: '/Catalogos/Colaborador/Buscar',
        type: "POST",
        data: { Nombre, IdPuesto, Estatus }
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
            $("#divListaColaborador").html(data.ListaCat);
            $('#tblColaborador').DataTable({
                responsive: true,
                searching: true,
                ordering: true,
                scrollX: true,
                scrollY: true,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ Colaboradores",
                    "zeroRecords": "Ningun Colaborador Encontrado - valide su busqueda",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay Colaboradores disponibles",
                    "infoFiltered": "(filtrado de _MAX_ Colaboradores en total)",
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
    $("#txtNombre").val("");
    $("#sltPuesto").val('first').change();
    $("#sltEstatus").val('first').change();
}

function Editar(Id, IdPuesto, Nombre, APaterno, AMaterno, Telefono, EMail, FHCon, Estatus) {
    $("#txtIdColaboradorMEditar").val(Id);
    $("#sltPuestoMEditar").val(IdPuesto).change();
    $("#txtNombreMEditar").val(Nombre);
    $("#txtAPaternoMEditar").val(APaterno);
    $("#txtAMaternoMEditar").val(AMaterno);
    $("#txtTelefonoMEditar").val(Telefono);
    $("#txtEMailMEditar").val(EMail);
    $("#txtFecConMEditar").val(FHCon);

    if (Estatus == "Activo") {
        $("#sltEstatusModificar").val(1).change();
    }
    else {
        $("#sltEstatusModificar").val(0).change();
    }

    MostrarMenu();
    $('#ModificarModal').modal('show');
    $("#txtNombreMEditar").focus();
}

function EditarColaborador() {
    if (ValidarCamposEditar()) {
        var Id = parseInt($("#txtIdColaboradorMEditar").val());
        var IdPuesto = parseInt($("#sltPuestoMEditar").val());
        var Nombre = $("#txtNombreMEditar").val();
        var APaterno = $("#txtAPaternoMEditar").val();
        var AMaterno = $("#txtAMaternoMEditar").val();
        var Telefono = $("#txtTelefonoMEditar").val();
        var EMail = $("#txtEMailMEditar").val();
        var FhCon = $("#txtFecConMEditar").val();
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
            url: '/Catalogos/Colaborador/Editar',
            type: "POST",
            data: { Id, IdPuesto, Nombre, APaterno, AMaterno, Telefono, EMail, FhCon, Estatus }
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
                $("#divListaColaborador").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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

    var IdPuesto = $("#sltPuestoMEditar").val();
    var Nombre = $("#txtNombreMEditar").val();
    var APaterno = $("#txtAPaternoMEditar").val();
    var AMaterno = $("#txtAMaternoMEditar").val();
    var Telefono = $("#txtTelefonoMEditar").val();
    var EMail = $("#txtEMailMEditar").val();
    var FhCon = $("#txtFecConMEditar").val();

    if (IdPuesto === "" || IdPuesto === 0 || IdPuesto === null) {
        Lista[a] = "Puesto";
        a++;
    }
    if (Nombre === "") {
        Lista[a] = "Nombre";
        a++;
    }
    if (APaterno === "") {
        Lista[a] = "Apellido Paterno";
        a++;
    }
    if (AMaterno === "") {
        Lista[a] = "Apellido Materno";
        a++;
    }
    if (Telefono === "") {
        Lista[a] = "Telefono";
        a++;
    }
    if (EMail === "") {
        Lista[a] = "EMail";
        a++;
    }
    if (FhCon === "") {
        Lista[a] = "Fecha Contratacion";
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
            text: 'Es obligatorio capturar:' + Pendientes,
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
    $("#sltPuestoMEditar").val('first').change();
    $("#txtNombreMEditar").val("");
    $("#txtAPaternoMEditar").val("");
    $("#txtAMaternoMEditar").val("");
    $("#txtTelefonoMEditar").val("");
    $("#txtEMailMEditar").val("");
    $("#txtFecConMEditar").val("");

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
            url: '/Catalogos/Colaborador/Eliminar',
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
                $("#divListaColaborador").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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


function Documentos(Nombre, APaterno, AMaterno, Id) {
    $("#AltaDModalLabelB").text(Nombre + ' ' + APaterno + ' ' + AMaterno);
    $("#txtIdColaboradorMD").val(Id);
    
    MostrarMenu();
    $('#AltaDModal').modal('show');
    $("#sltDocumentoMD").val('first').change();    
}

function AltaDColaborador() {
    var Id = $("#txtIdColaboradorMD").val();
    var IdDocumento = $("#sltDocumentoMD").val();
    var Documento = $("#sltDocumentoMD option:selected").text();
    var IdRuta = $("#txtIdRutaMD").val();
    var Ruta = $("#txtRutaMD").val();

    if (ValidarDocumentos()) {
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
            url: '/Catalogos/Colaborador/CrearCarpeta',
            type: "POST",
            data: { Id, Documento, Ruta }
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
                var Archivos = $("#iptArchivos").get(0).files;
                var ArchivosData = new FormData();
                for (var i = 0; i < Archivos.length; i++) {
                    ArchivosData.append("iptArchivos", Archivos[i]);
                }
                $.ajax({
                    url: "/Catalogos/Colaborador/AgregarArchivo",
                    type: "POST",
                    autoUpload: true,
                    contentType: false,
                    processData: false,
                    data:  ArchivosData 
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
                            text: 'Guardando...',
                            showHideTransition: 'slide',
                            hideAfter: 3000,
                            icon: 'info',
                            loaderBg: '#46c35f',
                            position: 'top-right'
                        })
                        $.ajax({
                            url: '/Catalogos/Colaborador/GuardarD',
                            type: "POST",
                            data: { Id, IdDocumento, Documento, IdRuta, Ruta }
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
                                    text: 'Los archivos se han Guardado correctamente',
                                    showHideTransition: 'slide',
                                    hideAfter: 6000,
                                    icon: 'success',
                                    loaderBg: '#f96868',
                                    position: 'top-right'
                                })
                                $("#divListaColaborador").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
                                $("#AltaDModal").modal("hide");
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
    var IdDocumento = $("#sltDocumentoMD").val();
    var Archivos = $("#iptArchivos").get(0).files;
    var Final = true;
    var Documento = true;
    var Archivo = true;
    if (IdDocumento === 0 || IdDocumento === null) {
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

function LimpiarCamposD() {
    $("#sltDocumentoMD").val('first').change();
    $("#iptArchivos").val('');
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