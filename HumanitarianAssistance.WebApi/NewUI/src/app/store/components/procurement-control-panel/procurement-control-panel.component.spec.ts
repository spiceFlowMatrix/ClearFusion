import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcurementControlPanelComponent } from './procurement-control-panel.component';

describe('ProcurementControlPanelComponent', () => {
  let component: ProcurementControlPanelComponent;
  let fixture: ComponentFixture<ProcurementControlPanelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProcurementControlPanelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcurementControlPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
