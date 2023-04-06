$(document).ready(function () {
    $(".js-example-basic-single").select2();
    $(".js-example-basic-multiple").select2();

    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    Lectura(true);
    $("#btnGuardar").hide();
    $("#btnEditar").show();
    BuscarUbica();
    $("#divUbicaC").hide();
    $("#btnGEditar").hide();
});

function BuscarUbica() {
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
        url: '/Catalogos/Cuenta/Buscar',
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
            $.toast({
                heading: 'Busqueda',
                text: 'La busqueda se realizo correctamente',
                showHideTransition: 'slide',
                hideAfter: 5000,
                icon: 'success',
                loaderBg: '#f96868',
                position: 'top-right'
            })
            $("#txtNombreM").val(data.Cliente.Nombre);
            $("#txtAPaternoM").val(data.Cliente.APaterno);
            $("#txtAMaternoM").val(data.Cliente.AMaterno);
            $("#txtTelefonoM").val(data.Cliente.Telefono);
            $("#txtEMailM").val(data.Cliente.EMail);
            $("#divListaUbicaciones").html(data.ListaCat);
            $('#tblUbicacion').DataTable({
                //responsive: false,
                searching: true,
                ordering: true,
                scrollX: true,
                scrollY: true,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ Ubicaciones",
                    "zeroRecords": "Ninguna Ubicacion Encontrada - valide su busqueda",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay ubicacion disponible",
                    "infoFiltered": "(filtrado de _MAX_ ubicaciones en total)",
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

function EditarInfo() {
    Lectura(false);
    $("#btnGuardar").show();
    $("#btnEditarInfo").hide();
}

function Lectura(Estatus) {
    $("#txtNombreM").attr('readonly', Estatus);
    $("#txtAPaternoM").attr('readonly', Estatus);
    $("#txtAMaternoM").attr('readonly', Estatus);
    $("#txtTelefonoM").attr('readonly', Estatus);
    $("#txtEMailM").attr('readonly', Estatus);
}

function GuardarInfo() {
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

    var Nombre = $("#txtNombreM").val();
    var APaterno = $("#txtAPaternoM").val();
    var AMaterno = $("#txtAMaternoM").val();
    var Telefono = $("#txtTelefonoM").val();
    var EMail = $("#txtEMailM").val();

    $.ajax({
        url: '/Catalogos/Cuenta/ActualizarCliente',
        type: "POST",
        data: { Nombre, APaterno, AMaterno, Telefono, EMail }

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
                heading: 'Actualizado',
                text: 'El registro se ha actualizado correctamente',
                showHideTransition: 'slide',
                hideAfter: 6000,
                icon: 'success',
                loaderBg: '#f96868',
                position: 'top-right'
            })
            $("#btnGuardar").hide();
            $("#btnEditarInfo").show();
            Lectura(true);
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

function AgregarUbica() {
    $("#btnAgregar").hide();
    $("#divUbicaC").show();
    $("#divDireccion").hide();
    $("#divDireccionDetalle").hide();
    $("#divDireccionF").hide();
    $("#divCalles").hide();
    $("#divSaveUbica").hide();
    window.scrollBy(0, 500); // horizontal and vertical scroll increments    
}

function BuscarCP() {
    if (ValidarCP()) {
        var CP = $("#txtCPMUbicacion").val();
        $("#txtCPMUbicacion").prop('readonly', true);
        $("#txtEstadoMUbicacion").val('');
        $("#txtMpioMUbicacion").val('');
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
            url: '/Catalogos/Cuenta/BuscarEdoyMpio',
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
                OcultarUbica();
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
                $("#txtCPMUbicacion").val('');
                $("#txtCPMUbicacion").prop('readonly', false);
            }
            else {
                $("#txtEstadoMUbicacion").val(data.Edo);
                $("#txtMpioMUbicacion").val(data.Mpio);

                $.ajax({
                    url: '/Catalogos/Cuenta/BuscarColonias',
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
                        OcultarUbica();
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
                        $("#divSaveUbica").show();
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
    $("#divSaveUbica").hide();

    $("#txtCPMUbicacion").prop('readonly', false);

    $("#txtEstadoMUbicacion").val("");
    $("#txtMpioMUbicacion").val("");

    $("#txtCPMUbicacion").val("");
}

function OcultarUbica() {
    $("#btnAgregar").show();
    $("#divUbicaC").hide();
    window.scrollBy(0, 0); // horizontal and vertical scroll increments    
}

function GuardarCDireccion() {
    if (ValidarCamposDireccion()) {

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
            url: '/Catalogos/Cuenta/GDireccion',
            type: "POST",
            data: {
                CP, Colonia, Calle, NExterior, NInterior, Nombre, Telefono
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
                OcultarUbica();
                $("#divListaUbicaciones").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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

function Editar(Id, Nombre, Telefono, CP, EntreCalle, YCalle, Calle, NExterior
    , NInterior, Colonia, Municipio, Estado, Descripcion) {

    $("#txtIdCUbicacion").val(Id);
    $("#txtNombreMUbicacion").val(Nombre);
    $("#txtTelefonoMUbicacion").val(Telefono);
    $("#txtCPMUbicacion").val(CP);
    BuscarCP();
    //$("#cmbColonia").val(Colonia).change();
    //$("#cmbColonia option:eq('" + Colonia + "')").prop('selected', true);
    //$('#cmbColonia option[value="' + Colonia + '"]').prop('selected', true).change();


    //$("#cmbColonia").val(Colonia); // Select the option with a value of '1'
    //$("#cmbColonia").trigger('change'); // Notify any JS components that the value changed
    //$("#cmbColonia").val('"' + Colonia + '"').trigger('change.select2');
    $("#cmbColonia").val(Colonia).trigger("change");
    

    //$("#cmbColonia").change();
    //$(".js-example-basic-single").select2();
    //$("#cmbColonia").select2().select2('val', Colonia);


    $("#txtCalleMUbicacion").val(Calle);
    $("#txtNExteriorMUbicacion").val(NExterior);
    $("#txtNInteriorMUbicacion").val(NInterior);
    $("#txtEntreCalleMUbicacion").val(EntreCalle);
    $("#txtYCalleMUbicacion").val(YCalle);
    $("#txtDescripcionMUbicacion").val(Descripcion);


    $("#btnAgregar").hide();
    $("#divUbicaC").show();
    $("#divDireccion").hide();
    $("#divDireccionDetalle").hide();
    $("#divDireccionF").hide();
    $("#divCalles").hide();
    $("#divSaveUbica").hide();
    $("#btnEditar").hide();
    $("#btnGEditar").show();
    window.scrollBy(0, 500); // horizontal and vertical scroll increments    
}

function EditarUbicacion() {
    if (ValidarCamposEditar()) {
        var Id = $("#txtIdCUbicacion").val();
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
            url: '/Catalogos/Cuenta/Editar',
            type: "POST",
            data: { Id, Nombre, Telefono, CP, Colonia, Calle, NExterior, NInterior, EntreCalle, YCalle, Descripcion }
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
                OcultarUbica();
                $("#divListaUbicaciones").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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

function LimpiarCamposEditar() {
    $("#txtNombreMUbicacion").val('');
    $("#txtTelefonoMUbicacion").val('');
    $("#txtCPMUbicacion").val('');
    $("#cmbColonia").html('');
    $("#txtCalleMUbicacion").val('');
    $("#txtNExteriorMUbicacion").val('');
    $("#txtNInteriorMUbicacion").val('');
    $("#txtEntreCalleMUbicacion").val('');
    $("#txtYCalleMUbicacion").val('');
    $("#txtDescripcionMUbicacion").val('');
}

function Eliminar(Id) {
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
        url: '/Catalogos/Cuenta/Eliminar',
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
            OcultarUbica();
            $("#divListaUbicaciones").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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

