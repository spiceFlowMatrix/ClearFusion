import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddThreeReferenceDetailsComponent } from './add-three-reference-details.component';

describe('AddThreeReferenceDetailsComponent', () => {
  let component: AddThreeReferenceDetailsComponent;
  let fixture: ComponentFixture<AddThreeReferenceDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddThreeReferenceDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddThreeReferenceDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
