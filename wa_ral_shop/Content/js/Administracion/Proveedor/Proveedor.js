$(document).ready(function () {
    $(".js-example-basic-single").select2();
    $(".js-example-basic-multiple").select2();
    BuscarPais();
});

function BuscarPais() {
    resetToastPosition();
    //var ToastInfo =
    //$.toast({
    //    heading: 'Informaci&oacute;n',
    //    text: 'Buscando CP...',
    //    showHideTransition: 'slide',
    //    hideAfter: 3000,
    //    icon: 'info',
    //    loaderBg: '#46c35f',
    //    position: 'top-right'
    //})

    $.ajax({
        url: '/Administracion/Proveedor/BuscarPaises',
        type: "POST",
        data: {}
    }).done(function (data, textStatus, jqXHR) {
        if (data.Codigo === "Error") {
            resetToastPosition();
            $.toast({
                heading: 'Error',
                text: "Problemas para consultar", //data.mensaje,
                showHideTransition: 'slide',
                hideAfter: 8000,
                icon: 'error',
                loaderBg: '#f2a654',
                position: 'top-right'
            })
        }
        else {
            //resetToastPosition();
            //$.toast({
            //    heading: 'Consultado',
            //    text: 'CP Encontrado',
            //    showHideTransition: 'slide',
            //    hideAfter: 6000,
            //    icon: 'success',
            //    loaderBg: '#f96868',
            //    position: 'top-right'
            //})

            $.each(data.comboPaises, function (Id, Dato) {
                $("#cmbPais").append('<option value="' + Dato.Id + '">' + Dato.Dato + '</option>');
                $("#cmbPaisM").append('<option value="' + Dato.Id + '">' + Dato.Dato + '</option>');
                $("#cmbPaisMEditar").append('<option value="' + Dato.Id + '">' + Dato.Dato + '</option>');
            });
            $("#cmbPais").val('first').change();
            $("#cmbPaisM").val('first').change();
            $("#cmbPaisMEditar").val('first').change();
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
    })
}

function AltaProveedor() {
    if (ValidarCampos()) {
        var Nombre = $("#txtNombreM").val();
        var IdPais = $("#cmbPaisM").val();
        var Telefono = $("#txtTelefonoM").val();
        var Telefono2 = $("#txtTelefonoM2").val();
        var EMail = $("#txtEMailM").val();
        var Email2 = $("#txtEMailM2").val();
        var Estrellas = $("#sltEstrellas").val();
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
            url: '/Administracion/Proveedor/Alta',
            type: "POST",
            data: { Nombre, IdPais, Telefono, Telefono2, EMail, Email2, Estrellas }
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
                $("#divListaProveedor").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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

    var Nombre = $("#txtNombreM").val();
    var Pais = $("#cmbPaisM").val();
    var Telefono = $("#txtTelefonoM").val();
    //var Telefono2 = $("#txtTelefonoM2").val();
    var EMail = $("#txtEMailM").val();
    //var Email2 = $("#txtEMailM2").val();
    //var Comentario = $("#txtComentarioM").val();
    var Estrellas = $("#sltEstrellas").val();

    if (Nombre === "") {
        Lista[a] = "Nombre";
        a++;
    }
    if (Pais === "") {
        Lista[a] = "Pais";
        a++;
    }
    if (Telefono === "") {
        Lista[a] = "Telefono";
        a++;
    }
    //if (Telefono2 === "") {
    //    Lista[a] = "Telefono 2";
    //    a++;
    //}
    if (EMail === "") {
        Lista[a] = "EMail";
        a++;
    }
    //if (Email2 === "") {
    //    Lista[a] = "EMail 2";
    //}
    //if (Comentario === "") {
    //    Lista[a] = "Comentario";
    //}
    if (Estrellas === 0) {
        Lista[a] = "Estrellas";
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

function LimpiarCampos() {
    $("#txtNombreM").val("");
    $("#cmbPaisM").val(1).change();
    $("#txtTelefonoM").val("");
    $("#txtTelefonoM2").val("");
    $("#txtEMailM").val("");
    $("#txtEMailM2").val("");
    $("#sltEstrellas").barrating('set', 1);
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
    var IdPais = $("#cmbPais").val();
    var Estatus = $("#sltEstatus").val();
    if (IdPais === null) {
        IdPais = 0;
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
        url: '/Administracion/Proveedor/Buscar',
        type: "POST",
        data: { Nombre, IdPais, Estatus }
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
            $("#divListaProveedor").html(data.ListaCat);
            $('#tblProveedor').DataTable({
                //responsive: false,
                searching: true,
                ordering: true,
                scrollX: true,
                scrollY: true,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ Proveedores",
                    "zeroRecords": "Ningún Proveedor Encontrado - valide su busqueda",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay Proveedor disponible",
                    "infoFiltered": "(filtrado de _MAX_ Proveedores en total)",
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
    $("#cmbPais").val('first').change();
    $("#sltEstatus").val('first').change();
}

function Editar(Id, Nombre, IdPais, Telefono, Telefono2, EMail, EMail2, Estrellas, EstatusSTR) {
    $("#txtIdProveedorMEditar").val(Id);
    $("#txtNombreMEditar").val(Nombre);
    $("#cmbPaisMEditar").val(IdPais).change();
    $("#txtTelefonoMEditar").val(Telefono);
    $("#txtTelefonoMEditar2").val(Telefono2);
    $("#txtEMailMEditar").val(EMail);
    $("#txtEMailMEditar2").val(EMail2);
    $("#sltEstrellasEditar").barrating('set', Estrellas);

    if (EstatusSTR == "Activo") {
        $("#sltEstatusModificar").val(1).change();
    }
    else {
        $("#sltEstatusModificar").val(0).change();
    }

    MostrarMenu();
    $('#ModificarModal').modal('show');
    $("#txtNombreMEditar").focus();
}

function EditarProveedor() {
    if (ValidarCamposEditar()) {

        var Id = parseInt($("#txtIdProveedorMEditar").val());
        var Nombre = $("#txtNombreMEditar").val();
        var IdPais = $("#cmbPaisMEditar").val();
        var Telefono = $("#txtTelefonoMEditar").val();
        var Telefono2 = $("#txtTelefonoMEditar2").val();
        var EMail = $("#txtEMailMEditar").val();
        var EMail2 = $("#txtEMailMEditar2").val();
        var Estrellas = $("#sltEstrellasEditar").val();
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
            url: '/Administracion/Proveedor/Editar',
            type: "POST",
            data: { Id, Nombre, IdPais, Telefono, Telefono2, EMail, EMail2, Estrellas, Estatus }
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
                $("#divListaProveedor").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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

    var Nombre = $("#txtNombreMEditar").val();
    var Pais = $("#cmbPaisMEditar").val();
    var Telefono = $("#txtTelefonoMEditar").val();
    //var Telefono2 = $("#txtTelefonoMEditar2").val();
    var EMail = $("#txtEMailMEditar").val();
    //var EMail2 = $("#txtEMailMEditar2").val();
    //var Comentario = $("#txtComentarioMEditar").val();
    //var Estatus = $("#sltEstatusModificar").val();
    var Estrellas = $("#sltEstrellasEditar").val();

    if (Nombre === "") {
        Lista[a] = "Nombre";
        a++;
    }
    if (Pais === "") {
        Lista[a] = "Pais";
        a++;
    }
    if (Telefono === "") {
        Lista[a] = "Telefono";
        a++;
    }
    //if (Telefono2 === "") {
    //    Lista[a] = "Telefono2";
    //    a++;
    //}
    if (EMail === "") {
        Lista[a] = "EMail";
        a++;
    }
    //if (EMail2 === "") {
    //    Lista[a] = "EMail2";
    //}
    //if (Comentario === "") {
    //    Lista[a] = "Comentario";

    //}
    if (Estrellas === 0) {
        Lista[a] = "Estrellas";
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
    $("#txtNombreMEditar").val("");
    $("#cmbPaisMEditar").val('first').change();
    $("#txtTelefonoMEditar").val("");
    $("#txtTelefonoMEditar2").val("");
    $("#txtEMailMEditar").val("");
    $("#txtEMailMEditar2").val("");
    $("#sltEstrellasEditar").barrating('set', 1);
    $("#sltEstatusModificar").val('first').change();
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
            url: '/Administracion/Proveedor/Eliminar',
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
                $("#divListaProveedor").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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