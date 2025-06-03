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
    const validIndices = this.columnsModified
      .map((col, idx) => col.length > 0 ? idx : -1)
      .filter(idx => idx >= 0);

    const filteredHeaders = validIndices.map(i => this.columnsModified[i]);
    const filteredDataCols = validIndices.map(i => this.dataModified[i]);

    const rowCount = filteredDataCols.length
      ? Math.max(...filteredDataCols.map(col => col.length))
      : 0;

    const rows = Array.from({ length: rowCount }, (_, rowIndex) =>
      filteredDataCols.map(col => col[rowIndex] ?? '')
    );

    const csvLines = [filteredHeaders, ...rows]
      .map(row => row.map(cell => cell.replace(/"/g, '""')).join(';'));

    const blob = new Blob(['\uFEFF' + csvLines.join('\r\n')], { type: `text/csv;charset=${encoding};` });
    return blob.arrayBuffer();
  }

}
