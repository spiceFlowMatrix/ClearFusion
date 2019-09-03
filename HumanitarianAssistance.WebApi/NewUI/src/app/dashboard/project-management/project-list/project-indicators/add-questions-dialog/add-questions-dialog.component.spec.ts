import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddQuestionsDialogComponent } from './add-questions-dialog.component';

describe('AddQuestionsDialogComponent', () => {
  let component: AddQuestionsDialogComponent;
  let fixture: ComponentFixture<AddQuestionsDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddQuestionsDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddQuestionsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
