import { Component, Input } from '@angular/core';
import { XMLTable } from '../../../interfaces/xmltable.interface';

@Component({
  selector: 'ui-xmltable',
  templateUrl: './xmltable.component.html',
  styleUrl: './xmltable.component.scss'
})
export class XMLTableComponent {
  @Input() table: XMLTable | null = null;
}
