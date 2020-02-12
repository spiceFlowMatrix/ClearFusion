import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddWeakPointsComponent } from './add-weak-points.component';

describe('AddWeakPointsComponent', () => {
  let component: AddWeakPointsComponent;
  let fixture: ComponentFixture<AddWeakPointsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddWeakPointsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddWeakPointsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
