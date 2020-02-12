import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddStrongPointsComponent } from './add-strong-points.component';

describe('AddStrongPointsComponent', () => {
  let component: AddStrongPointsComponent;
  let fixture: ComponentFixture<AddStrongPointsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddStrongPointsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddStrongPointsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
