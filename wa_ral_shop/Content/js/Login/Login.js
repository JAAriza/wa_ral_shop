$(document).ready(function () {
    //$("#UbicacionModal").modal("show");
});
function CrearCuenta() {
    $(".js-example-basic-single").select2();
    $(".js-example-basic-multiple").select2();
    LimpiarCampos();
    $("#AltaModal").modal("show");
}

function ViewPass() {
    var x = document.getElementById("txtPassM");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}

function AltaCliente() {
    if (ValidarCampos()) {
        var Nombre = $("#txtNombreM").val();
        var APaterno = $("#txtAPaternoM").val();
        var AMaterno = $("#txtAMaternoM").val();
        //var Estrellas = $("#sltEstrellas").val();
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
            url: '/Sesion/Alta',
            type: "POST",
            data: { Nombre, APaterno, AMaterno, Telefono, EMail, Pass }
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
                var NCompleto = $("#txtNombreM").val() + " " + $("#txtAPaternoM").val() + " " + $("#txtAMaternoM").val();
                var Id = data.Ide;
                LimpiarCampos();
                $("#AltaModal").modal("hide");

                Direccion(NCompleto, Telefono, Id);
                AgregarCustomer(Nombre, APaterno, AMaterno, Telefono, EMail, Id)
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
    //var Estrellas = $("#sltEstrellas").val();
    var Telefono = $("#txtTelefonoM").val();
    var EMail = $("#txtEMailM").val();

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
    //if (Estrellas === 0) {
    //    Lista[a] = "Estrellas";
    //    a++;
    //}
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

function LimpiarCampos() {
    $("#txtNombreM").val("");
    $("#txtAPaternoM").val("");
    $("#txtAMaternoM").val("");
    //$("#sltEstrellas").val(1).change();
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

function Direccion(NCompleto, Telefono, Ide) {
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
    $("#txtIdClienteMUbicacion").val(Ide);
    $("#txtNombreMUbicacion").val(NCompleto);
    $("#txtTelefonoMUbicacion").val(Telefono);
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

function AgregarCustomer(Nombre, APaterno, AMaterno, Telefono, EMail, Id) {
    $.ajax({
        url: '/Payment/AgregarCustomer',
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
        //Revisar logica porque aunque no encuentra datos si esta habilitando como si encontrara, igual y falta una validacion o asi
        $.ajax({
            url: '/Sesion/BuscarEdoyMpio',
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
                $("#txtCPMUbicacion").val("");
                $("#txtCPMUbicacion").prop('readonly', false);
                $("#divDireccion").hide();
                $("#divDireccionDetalle").hide();
                $("#divDireccionF").hide();
                $("#divCalles").hide();

            }
            else {
                $("#txtEstadoMUbicacion").val(data.Edo);
                $("#txtMpioMUbicacion").val(data.Mpio);

                $.ajax({
                    url: '/Sesion/BuscarColonias',
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
    $("#cmbColonia").html("");
    $("#txtCalleMUbicacion").val("");
    $("#txtNExteriorMUbicacion").val("");
    $("#txtNInteriorMUbicacion").val("");
    $("#txtEntreCalleMUbicacion").val("");
    $("#txtYCalleMUbicacion").val("");
    $("#txtDescripcionMUbicacion").val("");
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
            url: '/Sesion/GDireccion',
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
                $('#UbicacionModal').modal('hide');
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