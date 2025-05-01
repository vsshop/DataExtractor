import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AgGridModule } from 'ag-grid-angular';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { TableComponent } from './shared/sheet/table/table.component';
import { SheetComponent } from './shared/sheet/sheet.component';
import { CellButtonComponent } from './shared/sheet/table/cell-button/cell-button.component';
import { ButtonComponent } from './shared/button/button.component';
import { IconComponent } from './shared/icon/icon.component';
import { HttpClientModule } from '@angular/common/http';
import { UploaderComponent } from './shared/uploader/uploader.component';
import { CsvFileComponent } from './shared/csv-files/csv-file/csv-file.component';
import { CsvFilesComponent } from './shared/csv-files/csv-files.component';
import { ViewDirective } from './directives/view.directive';
import { ReviewComponent } from './pages/review/review.component';
import { ReaderComponent } from './shared/reader/reader.component';
import { ReaderCheckComponent } from './shared/reader/reader-check/reader-check.component';
import { ReaderEditComponent } from './shared/reader/reader-edit/reader-edit.component';
import { ReaderInputComponent } from './shared/reader/reader-input/reader-input.component';
import { ReaderColumnComponent } from './shared/reader/reader-column/reader-column.component';
import { XMLReaderComponent } from './shared/xmlreader/xmlreader.component';
import { PanelComponent } from './shared/panel/panel.component';
import { XMLTableComponent } from './shared/xmlreader/xmltable/xmltable.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TableComponent,
    SheetComponent,
    CellButtonComponent,
    ButtonComponent,
    IconComponent,
    UploaderComponent,
    CsvFileComponent,
    CsvFilesComponent,
    ViewDirective,
    ReviewComponent,
    ReaderComponent,
    ReaderCheckComponent,
    ReaderEditComponent,
    ReaderInputComponent,
    ReaderColumnComponent,
    XMLReaderComponent,
    PanelComponent,
    XMLTableComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AgGridModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
