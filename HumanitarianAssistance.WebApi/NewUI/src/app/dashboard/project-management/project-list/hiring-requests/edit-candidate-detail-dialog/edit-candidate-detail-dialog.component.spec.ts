import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditCandidateDetailDialogComponent } from './edit-candidate-detail-dialog.component';

describe('EditCandidateDetailDialogComponent', () => {
  let component: EditCandidateDetailDialogComponent;
  let fixture: ComponentFixture<EditCandidateDetailDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditCandidateDetailDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditCandidateDetailDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
