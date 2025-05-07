import { CSVFile } from "./csv.file";
import { XMLFile } from "./xml.file";

export class TableFile {
  name: string;
  columnsOriginal: string[];
  columnsModified: string[];
  dataOriginal: string[][];
  dataModified: string[][];
  modifiers: boolean[] = [];

  constructor(xml: XMLFile, csv: CSVFile) {
    this.name = csv.name;
    this.columnsOriginal = xml.columns.map(col => col);
    this.columnsModified = xml.columns.map(col => col);
    this.modifiers = xml.columns.map(_ => false);
    this.dataOriginal = csv.columns.map(col => [...col]);
    this.dataModified = csv.columns.map(col => [...col]);
  }

  get count() {
    return this.columnsOriginal;
  }

  content(encoding: string = 'iso-8859-1') {
    const rowCount = Math.max(...this.dataModified.map(col => col.length));
    const rows = Array.from({ length: rowCount }, (_, rowIndex) =>
      this.dataModified.map(col => col[rowIndex] ?? '')
    );

    const csv = [this.columnsModified, ...rows].map(row =>
      row.map(cell => cell.replace(/"/g, '""')).join(';')
    ).join('\r\n');

    const blob = new Blob(['\uFEFF' + csv], { type: `text/csv;charset=${encoding};` });
    return blob.arrayBuffer();
  }
}
