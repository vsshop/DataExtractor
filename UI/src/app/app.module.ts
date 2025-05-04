import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AgGridModule } from 'ag-grid-angular';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { TableComponent } from './shared/components/tables/sheet/table/table.component';
import { SheetComponent } from './shared/components/tables/sheet/sheet.component';
import { ButtonComponent } from './shared/components/basic/button/button.component';
import { IconComponent } from './shared/components/basic/icon/icon.component';
import { HttpClientModule } from '@angular/common/http';
import { ViewDirective } from './shared/directives/view.directive';
import { ReviewComponent } from './pages/review/review.component';
import { XMLReaderComponent } from './shared/components/xmlreader/xmlreader.component';
import { PanelComponent } from './shared/components/basic/panel/panel.component';
import { CsvFileComponent } from './shared/components/data/csv-file/csv-file.component';
import { XMLFileComponent } from './shared/components/data/xml-file/xml-file.component';
import { UploaderComponent } from './shared/components/upload/uploader/uploader.component';
import { UploadComponent } from './shared/components/upload/upload.component';
import { TableFileComponent } from './shared/components/data/table-file/table-file.component';
import { TableManagerComponent } from './shared/components/tables/table-manager/table-manager.component';
import { TableManagerRowComponent } from './shared/components/tables/table-manager/table-manager-row/table-manager-row.component';
import { ModalComponent } from './shared/components/modal/modal.component';
import { ModalErrorsComponent } from './shared/components/modal/modal-errors/modal-errors.component';
import { ModalReviewComponent } from './shared/components/modal/modal-review/modal-review.component';
import { ModalEditComponent } from './shared/components/modal/modal-edit/modal-edit.component';

@NgModule({
  declarations: [
    AppComponent,

    HomeComponent,
    ReviewComponent,

    TableComponent,
    SheetComponent,
    ButtonComponent,
    IconComponent,
    UploaderComponent,
    CsvFileComponent,
    ViewDirective,
    ReviewComponent,
    XMLReaderComponent,
    PanelComponent,
    UploadComponent,
    XMLFileComponent,
    TableFileComponent,
    TableManagerComponent,
    TableManagerRowComponent,
    ModalComponent,
    ModalErrorsComponent,
    ModalReviewComponent,
    ModalEditComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule, BrowserAnimationsModule,
    FormsModule, AppRoutingModule,
    AgGridModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
