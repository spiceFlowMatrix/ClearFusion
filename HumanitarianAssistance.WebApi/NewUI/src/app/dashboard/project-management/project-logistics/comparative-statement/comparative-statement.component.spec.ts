import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ComparativeStatementComponent } from './comparative-statement.component';

describe('ComparativeStatementComponent', () => {
  let component: ComparativeStatementComponent;
  let fixture: ComponentFixture<ComparativeStatementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ComparativeStatementComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComparativeStatementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
