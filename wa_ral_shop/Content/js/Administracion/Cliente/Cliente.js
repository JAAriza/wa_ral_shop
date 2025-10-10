$(document).ready(function () {
    $(".js-example-basic-single").select2();
    $(".js-example-basic-multiple").select2();

    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
});

function AltaCliente() {
    if (ValidarCampos()) {
        var Nombre = $("#txtNombreM").val();
        var APaterno = $("#txtAPaternoM").val();
        var AMaterno = $("#txtAMaternoM").val();
        var Estrellas = $("#sltEstrellas").val();
        var Telefono = $("#txtTelefonoM").val();
        var EMail = $("#txtEMailM").val();
        var Pass = $("#txtPassM").val();

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
            url: '/Administracion/Cliente/Alta',
            type: "POST",
            data: { Nombre, APaterno, AMaterno, Estrellas, Telefono, EMail, Pass }
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
                AgregarCustomer(Nombre, APaterno, AMaterno, Telefono, EMail, data.Ide);

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
                $("#divListaCliente").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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
    var APaterno = $("#txtAPaternoM").val();
    var AMaterno = $("#txtAMaternoM").val();
    var Estrellas = $("#sltEstrellas").val();
    var Telefono = $("#txtTelefonoM").val();
    var EMail = $("#txtEMailM").val();
    var Pass = $("#txtPassM").val();


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
    if (Estrellas === 0) {
        Lista[a] = "Estrellas";
        a++;
    }
    if (Telefono === "") {
        Lista[a] = "Telefono";
        a++;
    }
    if (EMail === "") {
        Lista[a] = "EMail";
    }
    if (Pass === "") {
        Lista[a] = "Contraseña"
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

function AgregarCustomer(Nombre, APaterno, AMaterno, Telefono, EMail, Id) {
    $.ajax({
        url: '/Administracion/Payment/AgregarCustomer',
        type: "POST",
        data: { Nombre, APaterno, AMaterno, Telefono, EMail, Id }
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
    });
}

function LimpiarCampos() {
    $("#txtNombreM").val("");
    $("#txtAPaternoM").val("");
    $("#txtAMaternoM").val("");
    $("#sltEstrellas").val(1).change();
    $("#txtTelefonoM").val("");
    $("#txtEMailM").val("");
    $("#txtPassM").val("");
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
    var APaterno = $("#txtAPaterno").val();
    var AMaterno = $("#txtAMaterno").val();
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
        url: '/Administracion/Cliente/Buscar',
        type: "POST",
        data: { Nombre, APaterno, AMaterno, Estatus }
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
            $("#divListaCliente").html(data.ListaCat);
            $('#tblCliente').DataTable({
                //responsive: false,
                searching: true,
                ordering: true,
                scrollX: true,
                scrollY: true,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ Clientes",
                    "zeroRecords": "Ningún Cliente Encontrado - valide su busqueda",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay cliente disponible",
                    "infoFiltered": "(filtrado de _MAX_ clientes en total)",
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
    $("#txtAPaterno").val("");
    $("#txtAMaterno").val("");
    $("#sltEstatus").val('first').change();
}

function Editar(Id, Nombre, APaterno, AMaterno, Estrellas, Telefono, EMail, Estatus, CustomerID) {
    $("#txtIdClienteMEditar").val(Id);
    $("#txtNombreMEditar").val(Nombre);
    $("#txtAPaternoMEditar").val(APaterno);
    $("#txtAMaternoMEditar").val(AMaterno);
    $("#sltEstrellasEditar").barrating('set', Estrellas);
    $("#txtTelefonoMEditar").val(Telefono);
    $("#txtEMailMEditar").val(EMail);
    if (Estatus == "Activo") {
        $("#sltEstatusModificar").val(1).change();
    }
    else {
        $("#sltEstatusModificar").val(0).change();
    }
    $("#txtCustomerId").val(CustomerID);
    MostrarMenu();
    $('#ModificarModal').modal('show');
    $("#txtNombreMEditar").focus();
}

function EditarCliente() {
    if (ValidarCamposEditar()) {

        var Id = parseInt($("#txtIdClienteMEditar").val());
        var Nombre = $("#txtNombreMEditar").val();
        var APaterno = $("#txtAPaternoMEditar").val();
        var AMaterno = $("#txtAMaternoMEditar").val();
        var Estrellas = $("#sltEstrellasEditar").val();
        var Telefono = $("#txtTelefonoMEditar").val();
        var EMail = $("#txtEMailMEditar").val();
        var Estatus = $("#sltEstatusModificar").val() == "1" ? true : false;
        var CustomerId = $("#txtCustomerId").val();


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
            url: '/Administracion/Cliente/Editar',
            type: "POST",
            data: { Id, Nombre, APaterno, AMaterno, Estrellas, Telefono, EMail, Estatus }
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
                ActualizarCustomer(CustomerId, Nombre + ' ' + APaterno + ' ' + AMaterno, EMail, Telefono, Estatus == true ? "Activo" : "Inactivo");
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
                $("#divListaCliente").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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
    var APaterno = $("#txtAPaternoMEditar").val();
    var AMaterno = $("#txtAMaternoMEditar").val();
    var Estrellas = $("#sltEstrellasEditar").val();
    var Telefono = $("#txtTelefonoMEditar").val();
    var EMail = $("#txtEMailMEditar").val();

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
    if (Estrellas === 0) {
        Lista[a] = "Estrellas";
        a++;
    }
    if (Telefono === "") {
        Lista[a] = "Telefono";
        a++;
    }
    if (EMail === "") {
        Lista[a] = "EMail";
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
    $("#txtAPaternoMEditar").val("");
    $("#txtAMaternoMEditar").val("");
    $("#sltEstrellasEditar").barrating('set', 1);
    $("#txtTelefonoMEditar").val("");
    $("#txtEMailMEditar").val("");
    $("#sltEstatusEditar").val('first').change();
}

function Eliminar(Id, EstatusSTR, CustomerId, Nombre, Email, Telefono) {
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
            url: '/Administracion/Cliente/Eliminar',
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
                ActualizarCustomer(CustomerId, Nombre, Email, Telefono, EstatusSTR);
                $.toast({
                    heading: 'Eliminado',
                    text: 'El registro se ha Eliminado correctamente',
                    showHideTransition: 'slide',
                    hideAfter: 6000,
                    icon: 'success',
                    loaderBg: '#f96868',
                    position: 'top-right'
                })
                $("#divListaCliente").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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

function ActualizarCustomer(CustomerId, Nombre, Email, Telefono, EstatusSTR) {
    $.ajax({
        url: '/Administracion/Payment/ActualizarCustomer',
        type: "POST",
        data: { CustomerId, Nombre, Email, Telefono, EstatusSTR }
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
    });
}

function Direccion(Id, Nombre, APaterno, AMaterno, Telefono) {
    resetToastPosition();
    //var ToastInfo =
    $.toast({
        heading: 'Informaci&oacute;n',
        text: 'Cargando...',
        showHideTransition: 'slide',
        hideAfter: 3000,
        icon: 'info',
        loaderBg: '#46c35f',
        position: 'top-right'
    })
    var NomCompleto = Nombre + ' ' + APaterno + ' ' + AMaterno
    $("#txtIdClienteMUbicacion").val(Id);
    $("#txtNombreMUbicacion").val(NomCompleto);
    $("#txtTelefonoMUbicacion").val(Telefono);

    MostrarMenu();
    $('#UbicacionModal').modal('show');

    $("#divDireccion").hide();
    $("#divDireccionDetalle").hide();
    $("#divDireccionF").hide();
    $("#divCalles").hide();

    $("#txtCalleMUbicacion").val("");
    $("#txtNExteriorMUbicacion").val("");
    $("#txtNInteriorMUbicacion").val("");
    $("#txtEntreCalleMUbicacion").val("");
    $("#txtYCalleMUbicacion").val("");
    $("#txtDescripcionMUbicacion").val("");
}

function BuscarCP() {
    if (ValidarCP()) {
        var CP = $("#txtCPMUbicacion").val();
        $("#txtCPMUbicacion").prop('readonly', true);
        $("#txtEstadoMUbicacion").val();
        $("#txtMpioMUbicacion").val();
        resetToastPosition();
        //var ToastInfo =
        $.toast({
            heading: 'Informaci&oacute;n',
            text: 'Buscando CP...',
            showHideTransition: 'slide',
            hideAfter: 3000,
            icon: 'info',
            loaderBg: '#46c35f',
            position: 'top-right'
        })

        $.ajax({
            url: '/Administracion/CDireccion/BuscarEdoyMpio',
            type: "POST",
            data: { CP }
        }).done(function (data, textStatus, jqXHR) {
            if (data.Codigo === "Error") {
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
                $('#UbicacionModal').modal('hide');
                Ocultar();
            }
            if (data.Codigo === "SinDatos") {
                resetToastPosition();
                $.toast({
                    heading: 'Informaci&oacute;n',
                    text: 'Verifica el Codigo Postal',
                    showHideTransition: 'slide',
                    hideAfter: 3000,
                    icon: 'info',
                    loaderBg: '#46c35f',
                    position: 'top-right'
                })
                $("#txtCPMUbicacion").val();
                $("#txtCPMUbicacion").prop('readonly', false);
            }
            else {
                $("#txtEstadoMUbicacion").val(data.Edo);
                $("#txtMpioMUbicacion").val(data.Mpio);

                $.ajax({
                    url: '/Administracion/CDireccion/BuscarColonias',
                    type: "POST",
                    data: { CP }
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
                        $('#UbicacionModal').modal('hide');
                        Ocultar();
                    }
                    else {
                        //resetToastPosition();
                        $.toast({
                            heading: 'Consultado',
                            text: 'CP Encontrado',
                            showHideTransition: 'slide',
                            hideAfter: 6000,
                            icon: 'success',
                            loaderBg: '#f96868',
                            position: 'top-right'
                        })
                        $("#divDireccion").show();
                        $("#divDireccionDetalle").show();
                        $("#divDireccionF").show();
                        $("#divCalles").show();
                        $("#cmbColonia").html("");
                        $.each(data.comboColonias, function (Id, Dato) {
                            $("#cmbColonia").append('<option value="' + Dato.Dato + '">' + Dato.Dato + '</option>');
                        });
                        $("#cmbColonia").val('first').change();

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

function ValidarCP() {
    var Valido = false;
    var CP = $("#txtCPMUbicacion").val();
    
    if (CP === "" || CP < 1000) {
        Valido = false;
    }
    else {
        Valido = true;
    }
    if (!Valido) {
        swal({
            title: 'Informaci\u00f3n',
            text: 'Verifica el valor del Codigo Postal',
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

function LimpiarCP() {
    $("#divDireccion").hide();
    $("#divDireccionDetalle").hide();
    $("#divDireccionF").hide();
    $("#divCalles").hide();

    $("#txtCPMUbicacion").prop('readonly', false);

    $("#txtEstadoMUbicacion").val("");
    $("#txtMpioMUbicacion").val("");

    $("#txtCPMUbicacion").val("");
}

function GuardarCDireccion() {
    if (ValidarCamposDireccion()) {

        var IdCliente = $("#txtIdClienteMUbicacion").val();
        var Nombre = $("#txtNombreMUbicacion").val();
        var Telefono = $("#txtTelefonoMUbicacion").val();
        var CP = $("#txtCPMUbicacion").val();
        var Colonia = $("#cmbColonia").val();
        var Calle = $("#txtCalleMUbicacion").val();
        var NExterior = $("#txtNExteriorMUbicacion").val();
        var NInterior = $("#txtNInteriorMUbicacion").val();
        var EntreCalle = $("#txtEntreCalleMUbicacion").val();
        var YCalle = $("#txtYCalleMUbicacion").val();
        var Descripcion = $("#txtDescripcionMUbicacion").val();

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
            url: '/Administracion/CDireccion/GDireccion',
            type: "POST",
            data: {
                IdCliente, CP, Colonia, Calle, NExterior, NInterior, Nombre, Telefono
                , Descripcion, EntreCalle, YCalle
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
                    text: 'La direccion se ha Guardado correctamente',
                    showHideTransition: 'slide',
                    hideAfter: 6000,
                    icon: 'success',
                    loaderBg: '#f96868',
                    position: 'top-right'
                })
                //$("#divListaCliente").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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

function ValidarCamposDireccion() {
    var Lista = new Array();
    var a = 0;
    var Final = true;

    var Nombre = $("#txtNombreMUbicacion").val();
    var Telefono = $("#txtTelefonoMUbicacion").val();
    var CP = $("#txtCPMUbicacion").val();
    var Colonia = $("#cmbColonia").val();
    var Calle = $("#txtCalleMUbicacion").val();
    var NExterior = $("#txtNExteriorMUbicacion").val();
    
    if (Nombre === "") {
        Lista[a] = "Nombre";
        a++;
    }
    if (Telefono === "") {
        Lista[a] = "Telefono";
        a++;
    }
    if (CP === "") {
        Lista[a] = "Codigo Postal";
        a++;
    }
    if (Colonia === 0) {
        Lista[a] = "Colonia";
        a++;
    }
    if (Calle === "") {
        Lista[a] = "Calle";
        a++;
    }
    if (NExterior === "") {
        Lista[a] = "N. Exterior";
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

function LimpiarUbicacion() {
    $("#cmbColonia").val('first').change();
    $("#txtCalleMUbicacion").val("");
    $("#txtNExteriorMUbicacion").val("");
    $("#txtNInteriorMUbicacion").val("");
    $("#txtEntreCalleMUbicacion").val("");
    $("#txtYCalleMUbicacion").val("");
    $("#txtDescripcionMUbicacion").val("");
    LimpiarCP();
}

function ViewPass() {
    var x = document.getElementById("txtPassM");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
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