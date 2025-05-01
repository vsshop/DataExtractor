export class CSVFile {
  constructor(public columns: string[][]) { }

  static async read(file: File): Promise<CSVFile> {
    const text = await file.text();
    const lines = text.split(/\r?\n/).filter(line => line.trim() !== '');
    if (lines.length === 0) return new CSVFile([]);

    const rows = lines.map(line => line.split(',').map(cell => cell.trim()));
    const count = Math.max(...rows.map(r => r.length));
    const columns = Array.from({ length: count }, (_, i) =>
      rows.map(row => row[i] ?? '')
    );

    return new CSVFile(columns);
  }
}
