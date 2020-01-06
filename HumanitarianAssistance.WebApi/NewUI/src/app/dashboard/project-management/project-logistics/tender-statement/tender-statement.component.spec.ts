import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TenderStatementComponent } from './tender-statement.component';

describe('TenderStatementComponent', () => {
  let component: TenderStatementComponent;
  let fixture: ComponentFixture<TenderStatementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TenderStatementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TenderStatementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
