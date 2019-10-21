import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OpportunityControlComponent } from './opportunity-control.component';

describe('OpportunityControlComponent', () => {
  let component: OpportunityControlComponent;
  let fixture: ComponentFixture<OpportunityControlComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OpportunityControlComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OpportunityControlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
