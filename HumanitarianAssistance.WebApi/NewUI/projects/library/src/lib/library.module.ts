import { NgModule } from '@angular/core';
import { LibraryComponent } from './library.component';
import { InlineEditComponent } from './components/inline-edit/inline-edit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { InlineEditDeleteComponent } from './components/inline-edit-delete/inline-edit-delete.component';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { DeleteConfirmationComponent } from './components/delete-confirmation/delete-confirmation.component';
import { FileDropModule } from 'ngx-file-drop';
import { MatDialogModule } from '@angular/material/dialog';
import { DragAndDropComponent } from './components/drag-and-drop/drag-and-drop.component';
import { SearchDropdownComponent } from './components/search-dropdown/search-dropdown.component';
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { MatSelectModule } from '@angular/material/select';
import { ListingDeleteComponent } from './components/listing-delete/listing-delete.component';
import { DocumentListingComponent } from './components/document-listing/document-listing.component';
import { HumDropdownComponent } from './components/hum-dropdown/hum-dropdown.component';
import { TableComponent } from './components/table/table.component';
import { ButtonComponent } from './components/button/button.component';
import { ConfigCardComponent } from './components/config-card/config-card.component';
import { MatCardModule, MatDividerModule } from '@angular/material';


@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatChipsModule,
    MatInputModule,
    MatButtonModule,
    MatDialogModule,
    FileDropModule,
    MatSelectModule,
    NgxMatSelectSearchModule,
    MatButtonModule,
    MatCardModule,
    MatDividerModule
  ],
  declarations: [
    LibraryComponent,
    InlineEditComponent,
    InlineEditDeleteComponent,
    DeleteConfirmationComponent,
    DragAndDropComponent,
    SearchDropdownComponent,
    ListingDeleteComponent,
    DocumentListingComponent,
    HumDropdownComponent,
    TableComponent,
    ButtonComponent,
    ConfigCardComponent
  ],
  exports: [
    LibraryComponent,
    InlineEditComponent,
    InlineEditDeleteComponent,
    DeleteConfirmationComponent,
    DragAndDropComponent,
    SearchDropdownComponent,
    ListingDeleteComponent,
    HumDropdownComponent,
    TableComponent,
    ButtonComponent,
    ConfigCardComponent
  ],
  entryComponents: [
    DeleteConfirmationComponent
  ]
})
export class LibraryModule { }
