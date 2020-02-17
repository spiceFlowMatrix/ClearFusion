import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContractClausesComponent } from './contract-clauses.component';

describe('ContractClausesComponent', () => {
  let component: ContractClausesComponent;
  let fixture: ComponentFixture<ContractClausesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContractClausesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContractClausesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
