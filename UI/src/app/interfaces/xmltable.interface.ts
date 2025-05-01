export class XMLTable {
  name: string;
  columns: string[] = [];

  constructor(table: Element) {
    this.name = this.get(table, "Name");
    const columns = Array.from(table.getElementsByTagName('VariableColumn'));
    this.columns = columns.map(column => this.get(column, "Name"));
  }

  private get(column: Element, tag: string) {
    return column.getElementsByTagName(tag)[0].textContent?.trim() ?? ''
  }
}
