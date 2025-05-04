import { AfterViewInit, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { Table } from '@interfaces/table.interface';

@Component({
  selector: 'ui-sheet',
  templateUrl: './sheet.component.html',
  styleUrl: './sheet.component.scss'
})
export class SheetComponent implements OnInit {
  @Input() tables: Table<string[]>[] = [];
  @Input() highlights: boolean[][][] = [];
  highlight: boolean[][] = [];
  select: Table<string[]> | null = null;

  constructor(private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    if (this.tables.length > 0) {
      this.select = this.tables[0];
    }
  }

  open(table: Table<string[]>, index: number) {
    this.select = table;
    this.highlight = this.highlights[index];
    this.cdr.detectChanges();
  }
}
