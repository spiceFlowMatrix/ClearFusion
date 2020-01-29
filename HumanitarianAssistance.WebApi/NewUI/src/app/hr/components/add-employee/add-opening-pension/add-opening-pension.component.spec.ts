import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOpeningPensionComponent } from './add-opening-pension.component';

describe('AddOpeningPensionComponent', () => {
  let component: AddOpeningPensionComponent;
  let fixture: ComponentFixture<AddOpeningPensionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddOpeningPensionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddOpeningPensionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
