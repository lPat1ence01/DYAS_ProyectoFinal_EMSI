using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using EMSI_Corporation.Models;
using EMSI_Corporation.Data;
using System;
using System.Linq;

namespace EMSI_Corporation.Views.Inventario
{
    public class InventarioPDF
    {
        private readonly AppDBContext _context;

        public InventarioPDF(AppDBContext context)
        {
            _context = context;
        }

        public IDocument CreatePDF()
        {
            var extintores = _context.Extintores.ToList();

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    // CABECERA
                    page.Header().Row(row =>
                    {
                        row.ConstantItem(140).Border(1).Height(60).Placeholder();

                        row.RelativeItem().Border(1).Column(col =>
                        {
                            col.Item().AlignCenter().Text("Existencia").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("Almacén Principal").FontSize(9);
                            col.Item().AlignCenter().Text(DateTime.Now.ToString("dd/MM/yyyy")).FontSize(9).Italic();
                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("CORPORACIÓN EMSI").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("Mza. B Lote. 14 Pp.Jj Baños Punta").FontSize(9);
                            col.Item().AlignCenter().Text("964214945 / 944259981").FontSize(9);
                            col.Item().AlignCenter().Text("corporacion.emsi@gmail.com").FontSize(9);
                        });
                    });

                    // CUERPO CON TABLA
                    page.Content().Table(table =>
                    {
                        // ENCABEZADO
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(); // ID
                            columns.RelativeColumn(); // Tipo Agente
                            columns.RelativeColumn(); // Clase Fuego
                            columns.RelativeColumn(); // Capacidad
                            columns.RelativeColumn(); // Vencimiento
                            columns.RelativeColumn(); // Estado
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("ID").Bold();
                            header.Cell().Element(CellStyle).Text("Tipo de Agente").Bold();
                            header.Cell().Element(CellStyle).Text("Clase de Fuego").Bold();
                            header.Cell().Element(CellStyle).Text("Capacidad (KG)").Bold();
                            header.Cell().Element(CellStyle).Text("Vencimiento").Bold();
                            header.Cell().Element(CellStyle).Text("Estado").Bold();

                            static IContainer CellStyle(IContainer container)
                            {
                                return container.DefaultTextStyle(x => x.FontSize(10))
                                                .PaddingVertical(5)
                                                .AlignCenter()
                                                .Background(Colors.Grey.Lighten2)
                                                .BorderBottom(1);
                            }
                        });

                        // FILAS DE EXTINTORES
                        foreach (var ext in extintores)
                        {
                            table.Cell().Element(CellStyle).Text(ext.IdExtintor.ToString());
                            table.Cell().Element(CellStyle).Text(ext.TipoAgente);
                            table.Cell().Element(CellStyle).Text(ext.ClaseFuego);
                            table.Cell().Element(CellStyle).Text($"{ext.CapacidadKG} KG");
                            table.Cell().Element(CellStyle).Text(ext.FechaVencimiento.ToString("yyyy-MM-dd"));
                            table.Cell().Element(CellStyle).Text(ext.Estado);

                            static IContainer CellStyle(IContainer container)
                            {
                                return container.DefaultTextStyle(x => x.FontSize(9))
                                                .PaddingVertical(3)
                                                .AlignCenter()
                                                .BorderBottom(0.5f);
                            }
                        }
                    });
                });
            });
        }
    }
}
