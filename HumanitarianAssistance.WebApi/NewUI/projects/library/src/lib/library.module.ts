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
    NgxMatSelectSearchModule
  ],
  declarations: [
    LibraryComponent,
    InlineEditComponent,
    InlineEditDeleteComponent,
    DeleteConfirmationComponent,
    DragAndDropComponent,
    SearchDropdownComponent,
    ListingDeleteComponent,
    DocumentListingComponent
  ],
  exports: [
    LibraryComponent,
    InlineEditComponent,
    InlineEditDeleteComponent,
    DeleteConfirmationComponent,
    DragAndDropComponent,
    SearchDropdownComponent,
    ListingDeleteComponent
  ],
  entryComponents: [
    DeleteConfirmationComponent
  ]
})
export class LibraryModule { }
