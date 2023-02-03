
$(document).ready(function () {
    $(".js-example-basic-single").select2();
   
    $(".js-example-basic-single").val('first').change();  

    $('.input-number').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });   

});

function Buscar() {
    var FI = $("#iptFI").val();
    var FF = $("#iptFF").val();
    var Estatus = $("#sltEstatus").val();
    resetToastPosition();
    //var ToastInfo =

    if (ValidarCamposBusqueda()) {
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
            url: '/Administracion/Compra/Buscar',
            type: "POST",
            data: { FI, FF, Estatus }
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
                $("#divListaCompra").html(data.ListaCat);
                $('#tblCompra').DataTable({
                    //responsive: false,
                    searching: true,
                    ordering: true,
                    scrollX: true,
                    scrollY: true,
                    "language": {
                        "lengthMenu": "Mostrar _MENU_ Compras",
                        "zeroRecords": "Ninguna Compra Encontrada - valide su busqueda",
                        "info": "Mostrando página _PAGE_ de _PAGES_",
                        "infoEmpty": "No hay Compras disponibles",
                        "infoFiltered": "(filtrado de _MAX_ Compras en total)",
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
}

function ValidarFechasBusqueda(FI, FF) {
    var Valido = false;

    if (ValidarFecha(FI) && ValidarFecha(FF)) {
        Valido = true;
    }
    else {
        Valido = false;
    }
    return Valido;
}

function ValidarFecha(Fecha) {
    var Valido = false;
    var str = Fecha;
    var year = str.match(/\/(\d{4})/)[1];
    var currYear = new Date().toString().match(/(\d{4})/)[1];
    if (year >= 2020 && year <= currYear) {
        Valido = true;
        //alert(year + ' es correcto.');
    } else {
        Valido = false;
        //alert(year + ' is an invalid year !');
    }
    return Valido;
}

function ValidarCamposBusqueda() {
    var FI = $("#iptFI").val();
    var FF = $("#iptFF").val();
    
    var Valido = true;
    if ((FI === "" && FF !== "") || (FI !== "" && FF === "")) {
        Valido = false;
        swal({
            title: 'Informaci\u00f3n',
            text: 'Necesitas capturar ambas fechas o ninguna para poder realizar la busqueda',
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
    else {
        if (FF === "" && FI === "") {
            Valido = true;
        }
        else {
            if (ValidarFechasBusqueda(FI, FF)) {
                Valido = true;
            }
            else {
                Valido = false;
            }
        }
    }
    
    return Valido;
}

function LimpiarCamposBusqueda() {
    $("#iptFI").val("");
    $("#iptFF").val("");
    $("#sltEstatus").val('first').change();
}

function AgregarDetalle() {
    if (ValidarDetalle()) {

        var Producto = $("#sltProductoM").val();
        var NomProd = $("#sltProductoM").text();
        var Cantidad = $("#txtCantidadM").val();
        var Proveedor = $("#sltProveedorM").val();
        var NomProv = $("#sltProveedorM").text();
        var CUnitario = $("#txtCUnitarioM").val();
        var CEnvio = $("#txtCEnvioDM").val();
        var FHCompra = $("#txtCompraM").val();
        var FHUSA = $("#txtLlegadaUSAM").val();

        $("#tblDetCompra").find('tbody').append('<tr>' +
            '<td class="pl-0" nowrap hidden="hidden">' + Producto + '</td>' +
            '<td class="pl-0" nowrap>' + NomProd + '</td>' +
            '<td class="pl-0 text-center" nowrap>' + Cantidad + '</td>' +
            '<td class="pl-0" nowrap hidden="hidden">' + Proveedor + '</td>' +
            '<td class="pl-0" nowrap>' + NomProv + '</td>' +
            '<td class="pl-0 text-center" nowrap>' + CUnitario + '</td>' +
            '<td class="pl-0 text-center" nowrap>' + CEnvio + '</td>' +
            '<td class="pl-0 text-center" nowrap>' + FHCompra + '</td>' +
            '<td class="pl-0 text-center" nowrap>' + FHUSA + '</td>' +
            '<td class="pr-0 text-center"><div class="badge badge-pill badge-primary text-center">' +
            '<i class="mdi mdi-pencil" onclick="EditRow(' + Producto + ',' + Cantidad + ',' + Proveedor +
            ',\'' + CUnitario + '\',\'' + CEnvio + '\',\'' + FHCompra + '\',\'' + FHUSA + '\', this);" style="cursor:pointer" title ="Editar">' +
            '</i></div>' +
            '<div class="badge badge-pill badge-warning text-center">' +
            '<i class="mdi mdi-trash-can" onclick="deleteRow(this)"style="cursor:pointer" title ="Eliminar"></i></div>' +
            '</td>' +
            '</tr>');

        LimpiarCamposDetalle();
    }
}

function ValidarDetalle() {

    var Lista = new Array();
    var a = 0;
    var Final = true;
    var Producto = $("#sltProductoM").val();
    var Cantidad = $("#txtCantidadM").val();
    var Proveedor = $("#sltProveedorM").val();
    var CUnitario = $("#txtCUnitarioM").val();
    var CEnvio = $("#txtCEnvioDM").val();
    var FHCompra = $("#txtCompraM").val();
    var FHUSA = $("#txtLlegadaUSAM").val();

    if (Producto === null || Producto === 0 || Producto === "0") {
        Lista[a] = "Producto";
        a++;
    }
    if (Cantidad === "") {
        Lista[a] = "Cantidad";
        a++;
    }
    if (Proveedor === null || Proveedor === 0 || Proveedor === "0") {
        Lista[a] = "Proveedor";
        a++;
    }
    if (CUnitario === "") {
        Lista[a] = "Costo Unitario";
        a++;
    }
    if (CEnvio === "") {
        Lista[a] = "Costo Envio";
        a++;
    }
    if (FHCompra === "") {
        Lista[a] = "Fecha Hora Compra";
        a++;
    }
    if (FHUSA === "") {
        Lista[a] = "Fecha Hora Llegada USA";
        a++;
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

function LimpiarCamposDetalle() {
    $("#sltProductoM").val('first').change();
    $("#txtCantidadM").val('');
    $("#sltProveedorM").val('first').change();
    $("#txtCUnitarioM").val('');
    $("#txtCEnvioDM").val('');
    $("#txtCompraM").val('');
    $("#txtLlegadaUSAM").val('');
}

function EditRow(Producto, Cantidad, Proveedor, CUnitario, CEnvio, FHCompra, FHUSA, btn) {

    $("#sltProductoM").val(Producto).change();
    $("#txtCantidadM").val(Cantidad);
    $("#sltProveedorM").val(Proveedor).change();
    $("#txtCUnitarioM").val(CUnitario);
    $("#txtCEnvioDM").val(CEnvio);
    $("#txtCompraM").val(FHCompra);
    $("#txtLlegadaUSAM").val(FHUSA);

    var row = btn.parentNode.parentNode.parentNode;
    row.parentNode.removeChild(row);
}

function deleteRow(btn) {    
    var row = btn.parentNode.parentNode.parentNode;
    row.parentNode.removeChild(row);
}


function AltaCompra() {

    if (ValidarCampos() && ValidarTabla()) {
        var Envio = $("#txtCEnvio").val();
        var Aviso = $("#txtAvisoImp").val();
        var Llegada = $("#txtLlegadaMX").val();
        var Total = $("#txtCTotal").val();
        var Estatus = $("#sltEstatusM").val();

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
            url: '/Administracion/Compra/Alta',
            type: "POST",
            data: {
                Envio, Aviso, Llegada, Total, Estatus
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
                    text: 'Se está procesando la informacion',
                    showHideTransition: 'slide',
                    hideAfter: 6000,
                    icon: 'success',
                    loaderBg: '#f96868',
                    position: 'top-right'
                })
                CrearTabla(data.idCompra);
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

function CrearTabla(IdCompra) {
    var table = $('#tblDetCompra').DataTable();
    var dd = table.rows().data().toArray();
    var data = new Array();

    $.each(dd, function (index, value) {

        var CompraDetalle = {};
        CompraDetalle.IdCompra = IdCompra;
        CompraDetalle.IdProducto = value[0];
        CompraDetalle.Cantidad = value[2];
        CompraDetalle.IdProveedor = value[3];
        var CU = value[5].split("$");
        CompraDetalle.CostoUnitario = CU[CU.length - 1].replace(",","");
        var CE = value[6].split("$");
        CompraDetalle.CostoEnvio = CE[CE.length - 1].replace(",","");
        CompraDetalle.FechaHoraCompra = value[7];
        CompraDetalle.FechaHoraLlegadaUSA = value[8];
        data.push(CompraDetalle);
    });

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
        url: '/Administracion/Compra/AltaDetalle',
        type: "POST",
        contentType: "application/json;",
        data: JSON.stringify(data)        
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
                url: '/Administracion/Compra/FalloGuardarDet',
                type: "POST",
                data: {
                    IdCompra
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
                        heading: 'Eliminado',
                        text: 'Sucedio un problema al guardar los datos, contacte a soporte',
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
            LimpiarCamposDetalle();
            
            $("#AltaModal").modal("hide");
            $("#divListaCompra").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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
        $("#tbodytblDetCompra").html('');
    });

}

function ValidarCampos() {
    var Lista = new Array();
    var a = 0;
    var Final = true;

    var Envio = $("#txtCEnvio").val();
    var Aviso = $("#txtAvisoImp").val();
    var Llegada = $("#txtLlegadaMX").val();
    var Total = $("#txtCTotal").val();
    var Estatus = $("#sltEstatusM").val();

    if (Envio === null || Envio === "") {
        Lista[a] = "Costo de Envio";
        a++;
    }
    if (Aviso === null || Aviso === "") {
        Lista[a] = "Aviso Importacion";
        a++;
    }
    if (Llegada === null || Llegada === "") {
        Lista[a] = "Fecha Llegada MX";
        a++;
    }
    if (Total === 0 || Total === "" || Total === null) {
        Lista[a] = "Costo Total";
        a++;
    }
    if (Estatus === null || Estatus === 0) {
        Lista[a] = "Estatus";
        a++;
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

function ValidarTabla() {
    var Campos = false;

    var table = $('#tblDetCompra').DataTable();
    var dd = table.rows().data().toArray();
    if (dd.length > 0) {
        Campos = true;
    }
    else {
        swal({
            title: 'Informaci\u00f3n',
            text: 'Es obligatorio capturar detalle de compra',
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

    return Campos;
}

function LimpiarCampos() {
    $("#txtCEnvio").val("");
    $("#txtAvisoImp").val("");
    $("#txtLlegadaMX").val("");
    $("#txtCTotal").val("");
    $("#sltEstatusM").val("");
}

function LimpiarTodo() {
    LimpiarCampos();
    LimpiarCamposDetalle();
    $("#tbodytblDetCompra").html('');
}

function Editar(Id,CostoEnvio, FechaHoraAvisoImportacion, FechaHoraLlegadaMX, CostoTotal, Estatus) {
    $("#txtIdCompraMEditar").val(Id);
    $("#txtCEnvioMEditar").val(CostoEnvio);
    $("#txtAvisoImpMEditar").val(FechaHoraAvisoImportacion);
    $("#txtLlegadaMXMEditar").val(FechaHoraLlegadaMX);
    $("#txtCTotalMEditar").val(CostoTotal);
    $("#sltEstatusMEditar").val(Estatus).change();

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
        url: '/Administracion/Compra/ObtenerDetalle',
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

            $("#tbodytblDetCompraMEditar").empty();//.remove();//.html("");
            $.each(data.ldet, function (Id, Dato) {
                var FHCompra = moment(Dato.FechaHoraCompra).format('DD-MM-YYYY HH:MM:SS');
                var FHUSA = moment(Dato.FechaHoraLlegadaUSA).format('DD-MM-YYYY HH:MM:SS');
                $("#tblDetCompraMEditar").find('tbody').append('<tr>' +
                    '<td class="pl-0" nowrap hidden="hidden">' + Dato.IdProducto + '</td>' +
                    '<td class="pl-0" nowrap>' + Dato.ProductoA.Nombre + '</td>' +
                    '<td class="pl-0 text-center" nowrap>' + Dato.Cantidad + '</td>' +
                    '<td class="pl-0" nowrap hidden="hidden">' + Dato.IdProveedor + '</td>' +
                    '<td class="pl-0" nowrap>' + Dato.ProveedorA.Nombre + '</td>' +
                    '<td class="pl-0 text-center" nowrap>' + Dato.CostoUnitario + '</td>' +
                    '<td class="pl-0 text-center" nowrap>' + Dato.CostoEnvio + '</td>' +
                    '<td class="pl-0 text-center" nowrap>' + FHCompra + '</td>' +
                    '<td class="pl-0 text-center" nowrap>' + FHUSA + '</td>' +
                    '<td class="pr-0 text-center"><div class="badge badge-pill badge-primary text-center">' +
                    '<i class="mdi mdi-pencil" onclick="EditRowEditar(' + Dato.IdProducto + ',' + Dato.Cantidad +
                    ',' + Dato.IdProveedor + ',\'' + Dato.CostoUnitario + '\',\'' + Dato.CostoEnvio + '\',\'' +
                    FHCompra + '\',\'' + FHUSA + '\', this);" style="cursor:pointer" title ="Editar">' +
                    '</i></div>' +
                    '<div class="badge badge-pill badge-warning text-center">' +
                    '<i class="mdi mdi-trash-can" onclick="deleteRowEditar(this)"style="cursor:pointer" title ="Eliminar"></i></div>' +
                    '</td>' +
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
        
    MostrarMenu();
    $('#EditarModal').modal('show');
    $("#txtCEnvioMEditar").focus();
}

function EditarCompra() {
    if (ValidarCamposEditar()) {

        var IdCompra = $("#txtIdCompraMEditar").val();
        var CE = $("#txtCEnvioMEditar").val().split("$");
        var Envio = CE[CE.length-1].replace(",", "");
        var Aviso = $("#txtAvisoImpMEditar").val();
        var Llegada = $("#txtLlegadaMXMEditar").val();
        var CT = $("#txtCTotalMEditar").val().split("$");
        var Total = CT[CT.length-1].replace(",", "");
        var Estatus = $("#sltEstatusMEditar").val();

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
            url: '/Administracion/Compra/Editar',
            type: "POST",
            data: {
                IdCompra, Envio, Aviso, Llegada, Total, Estatus
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
                    text: 'Se está actualizando la informacion',
                    showHideTransition: 'slide',
                    hideAfter: 6000,
                    icon: 'success',
                    loaderBg: '#f96868',
                    position: 'top-right'
                })
                CrearTablaEditar(IdCompra);
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

    var Envio = $("#txtCEnvioMEditar").val();
    var Aviso = $("#txtAvisoImpMEditar").val();
    var Llegada = $("#txtLlegadaMXMEditar").val();
    var Total = $("#txtCTotalMEditar").val();
    var Estatus = $("#sltEstatusMEditar").val();

    if (Envio === null || Envio === "") {
        Lista[a] = "Costo de Envio";
        a++;
    }
    if (Aviso === null || Aviso === "") {
        Lista[a] = "Aviso Importacion";
        a++;
    }
    if (Llegada === null || Llegada === "") {
        Lista[a] = "Fecha Llegada MX";
        a++;
    }
    if (Total === 0 || Total === "" || Total === null) {
        Lista[a] = "Costo Total";
        a++;
    }
    if (Estatus === null || Estatus === 0) {
        Lista[a] = "Estatus";
        a++;
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
    $("#txtCEnvioMEditar").val("");
    $("#txtAvisoImpMEditar").val("");
    $("#txtLlegadaMXMEditar").val("");
    $("#txtCTotalMEditar").val("");
    $("#sltEstatusMEditar").val('first').change();
}

function LimpiarCamposDetalleEditar() {
    $("#sltProductoMEditar").val('first').change();
    $("#txtCantidadMEditar").val('');
    $("#sltProveedorMEditar").val('first').change();
    $("#txtCUnitarioMEditar").val('');
    $("#txtCEnvioDMEditar").val('');
    $("#txtCompraMEditar").val('');
    $("#txtLlegadaUSAMEditar").val('');
}

function LimpiarTodoEditar() {
    LimpiarCamposEditar();
    LimpiarCamposDetalleEditar();
    $("#tbodytblDetCompraMEditar").html('');
}

function EditRowEditar(Producto, Cantidad, Proveedor, CUnitario, CEnvio, FHCompra, FHUSA, btn) {

    $("#sltProductoMEditar").val(Producto).change();
    $("#txtCantidadMEditar").val(Cantidad);
    $("#sltProveedorMEditar").val(Proveedor).change();
    $("#txtCUnitarioMEditar").val(CUnitario);
    $("#txtCEnvioDMEditar").val(CEnvio);
    $("#txtCompraMEditar").val(FHCompra);
    $("#txtLlegadaUSAMEditar").val(FHUSA);

    var row = btn.parentNode.parentNode.parentNode;
    row.parentNode.removeChild(row);
}

function deleteRowEditar(btn) {
    var row = btn.parentNode.parentNode.parentNode;
    row.parentNode.removeChild(row);
}

function CrearTablaEditar(IdCompra) {
    var table = $('#tblDetCompraMEditar').DataTable();
    var dd = table.rows().data().toArray();
    var data = new Array();

    $.each(dd, function (index, value) {

        var CompraDetalle = {};
        CompraDetalle.IdCompra = IdCompra;
        CompraDetalle.IdProducto = value[0];
        CompraDetalle.Cantidad = value[2];
        CompraDetalle.IdProveedor = value[3];
        var CU = value[5].split("$");
        CompraDetalle.CostoUnitario = CU[CU.length - 1].replace(",", "");
        var CE = value[6].split("$");
        CompraDetalle.CostoEnvio = CE[CE.length - 1].replace(",", "");
        CompraDetalle.FechaHoraCompra = value[7];
        CompraDetalle.FechaHoraLlegadaUSA = value[8];
        data.push(CompraDetalle);
    });

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
        url: '/Administracion/Compra/EditarDetalle',
        type: "POST",
        contentType: "application/json;",
        data: JSON.stringify(data)
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
                url: '/Administracion/Compra/FalloGuardarDetEditar',
                type: "POST",
                data: {
                    IdCompra
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
                        heading: 'Eliminado',
                        text: 'Sucedio un problema al guardar los datos, contacte a soporte',
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
            $.toast({
                heading: 'Guardado',
                text: 'El registro se ha guardado correctamente',
                showHideTransition: 'slide',
                hideAfter: 6000,
                icon: 'success',
                loaderBg: '#f96868',
                position: 'top-right'
            })

            LimpiarCamposEditar();
            LimpiarCamposDetalleEditar();

            $("#EditarModal").modal("hide");
            $("#divListaCompra").html('<h4 class="card-title" style="text-align:center">Sin resultados para mostrar</h4>');
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
        $("#tbodytblDetCompraMEditar").html('');
    });

}

function AgregarDetalleEditar() {
    if (ValidarDetalleEditar()) {

        var Producto = $("#sltProductoMEditar").val();
        var NomProd = $("#sltProductoMEditar").text();
        var Cantidad = $("#txtCantidadMEditar").val();
        var Proveedor = $("#sltProveedorMEditar").val();
        var NomProv = $("#sltProveedorMEditar").text();
        var CUnitario = $("#txtCUnitarioMEditar").val();
        var CEnvio = $("#txtCEnvioDMEditar").val();
        var FHCompra = $("#txtCompraMEditar").val();
        var FHUSA = $("#txtLlegadaUSAMEditar").val();

        $("#tblDetCompraMEditar").find('tbody').append('<tr>' +
            '<td class="pl-0" nowrap hidden="hidden">' + Producto + '</td>' +
            '<td class="pl-0" nowrap>' + NomProd + '</td>' +
            '<td class="pl-0 text-center" nowrap>' + Cantidad + '</td>' +
            '<td class="pl-0" nowrap hidden="hidden">' + Proveedor + '</td>' +
            '<td class="pl-0" nowrap>' + NomProv + '</td>' +
            '<td class="pl-0 text-center" nowrap>' + CUnitario + '</td>' +
            '<td class="pl-0 text-center" nowrap>' + CEnvio + '</td>' +
            '<td class="pl-0 text-center" nowrap>' + FHCompra + '</td>' +
            '<td class="pl-0 text-center" nowrap>' + FHUSA + '</td>' +
            '<td class="pr-0 text-center"><div class="badge badge-pill badge-primary text-center">' +
            '<i class="mdi mdi-pencil" onclick="EditRowEditar(' + Producto + ',' + Cantidad + ',' + Proveedor +
            ',\'' + CUnitario + '\',\'' + CEnvio + '\',\'' + FHCompra + '\',\'' + FHUSA + '\', this);" style="cursor:pointer" title ="Editar">' +
            '</i></div>' +
            '<div class="badge badge-pill badge-warning text-center">' +
            '<i class="mdi mdi-trash-can" onclick="deleteRowEditar(this)"style="cursor:pointer" title ="Eliminar"></i></div>' +
            '</td>' +
            '</tr>');

        LimpiarCamposDetalleEditar();
    }
}

function ValidarDetalleEditar() {

    var Lista = new Array();
    var a = 0;
    var Final = true;
    var Producto = $("#sltProductoMEditar").val();
    var Cantidad = $("#txtCantidadMEditar").val();
    var Proveedor = $("#sltProveedorMEditar").val();
    var CUnitario = $("#txtCUnitarioMEditar").val();
    var CEnvio = $("#txtCEnvioDMEditar").val();
    var FHCompra = $("#txtCompraMEditar").val();
    var FHUSA = $("#txtLlegadaUSAMEditar").val();

    if (Producto === null || Producto === 0 || Producto === "0") {
        Lista[a] = "Producto";
        a++;
    }
    if (Cantidad === "") {
        Lista[a] = "Cantidad";
        a++;
    }
    if (Proveedor === null || Proveedor === 0 || Proveedor === "0") {
        Lista[a] = "Proveedor";
        a++;
    }
    if (CUnitario === "") {
        Lista[a] = "Costo Unitario";
        a++;
    }
    if (CEnvio === "") {
        Lista[a] = "Costo Envio";
        a++;
    }
    if (FHCompra === "") {
        Lista[a] = "Fecha Hora Compra";
        a++;
    }
    if (FHUSA === "") {
        Lista[a] = "Fecha Hora Llegada USA";
        a++;
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
        data: JSON.stringify(ComboAnonymous),
        contentType: "application/json; charset=utf-8",
        dataType: 'json'

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