import { Core } from "@core";
import { XMLFile } from "./xml.file";
import { FileState } from "@enums/file.state";

export class CSVFile {
  state: FileState = FileState.none;
  constructor(public name: string, public columns: string[][]) { }

  equals(other: CSVFile | XMLFile | null | undefined): boolean {
    return !!other && this.name.toLowerCase() === other.name.toLowerCase();
  }
  static async read(file: File, encoding: string = 'windows-1251', split: string = ';'): Promise<CSVFile> {
    const text = await Core.read(file, encoding);
    const name = file.name.replace(/\.[^/.]+$/, '');
    const lines = text.split(/\r?\n/).filter(line => line.trim() !== '');
    if (lines.length === 0) return new CSVFile(name, []);

    const rows = lines.map(line => line.split(split).map(cell => cell.trim()));
    const count = Math.max(...rows.map(r => r.length));
    const columns = Array.from({ length: count }, (_, i) => rows.map(row => row[i] ?? ''));

    return new CSVFile(name, columns);
  }
}
