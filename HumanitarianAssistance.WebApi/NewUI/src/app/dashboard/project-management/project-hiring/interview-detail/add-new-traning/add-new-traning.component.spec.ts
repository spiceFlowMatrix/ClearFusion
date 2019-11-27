import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddNewTraningComponent } from './add-new-traning.component';

describe('AddNewTraningComponent', () => {
  let component: AddNewTraningComponent;
  let fixture: ComponentFixture<AddNewTraningComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddNewTraningComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddNewTraningComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
