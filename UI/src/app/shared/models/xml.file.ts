import { Core } from "@core";
import { CSVFile } from "./csv.file";
import { FileState } from "@enums/file.state";

export class XMLFile {
  name: string;
  columns: string[] = [];
  state: FileState = FileState.none;
  constructor(table: Element) {
    this.name = this.get(table, "Name");
    const columns = Array.from(table.getElementsByTagName('VariableColumn'));
    this.columns = columns.map(column => this.get(column, "Name"));
  }

  equals(other: CSVFile | XMLFile | null | undefined): boolean {
    return !!other && this.name.toLowerCase() === other.name.toLowerCase();
  }

  static async read(file: File, encoding: string = 'iso-8859-1') {
    const xml = await Core.read(file, encoding);
    const doc = new DOMParser().parseFromString(xml, 'application/xml');
    const tables = Array.from(doc.getElementsByTagName('Table'));
    return tables.map(table => new XMLFile(table));
  }

  private get(column: Element, tag: string) {
    return column.getElementsByTagName(tag)[0].textContent?.trim() ?? ''
  }
}
