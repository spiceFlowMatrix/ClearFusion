import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCloseRelativeComponent } from './add-close-relative.component';

describe('AddCloseRelativeComponent', () => {
  let component: AddCloseRelativeComponent;
  let fixture: ComponentFixture<AddCloseRelativeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddCloseRelativeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCloseRelativeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
