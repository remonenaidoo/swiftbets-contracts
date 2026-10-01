// Generated from SwiftBets.Contracts. Do not edit; run `dotnet run --project tools/SwiftBets.Contracts.SchemaGen`.

export interface CouponPlacedV1 {
    couponId: string;
    punterId: string;
    betType: BetType;
    stake: Money;
    totalOdds: number;
    potentialPayout: Money;
    legs: CouponLegV1[];
    placedAt: string;
}

export type BetType = "single" | "accumulator";

/** An amount in minor units (cents) of an ISO 4217 currency. */
export interface Money {
    minorUnits: number;
    currency: string;
}

export interface CouponLegV1 {
    legId: string;
    fixtureId: string;
    marketId: string;
    selectionId: string;
    odds: number;
    offerVersion: number;
}

export interface ResultPublishedV1 {
    fixtureId: string;
    resultVersion: number;
    status: ResultStatus;
    homeGoals: number;
    awayGoals: number;
    publishedAt: string;
}

/** Ordered by precedence: a higher value outranks a lower one at the same result version. */
export type ResultStatus = "provisional" | "official" | "correction" | "void";

export interface CouponRejectedV1 {
    couponId: string;
    punterId: string;
    reasonCode: string;
    rejectedAt: string;
}

/** Full snapshot of a fixture's offer; OfferVersion increases on every price or status change. */
export interface FixtureChangedV1 {
    fixtureId: string;
    competition: string;
    homeTeam: string;
    awayTeam: string;
    kickoffAt: string;
    status: FixtureStatus;
    offerVersion: number;
    markets: MarketV1[];
    changedAt: string;
}

export type FixtureStatus = "scheduled" | "inPlay" | "finished" | "postponed";

export interface MarketV1 {
    marketId: string;
    type: MarketType;
    status: MarketStatus;
    selections: SelectionV1[];
}

export type MarketType = "matchResult" | "totalGoalsOverUnder25";

export type MarketStatus = "open" | "suspended" | "closed";

export interface SelectionV1 {
    selectionId: string;
    name: string;
    odds: number;
}

/** Stage one of settlement: one leg judged against one version of its fixture's result. Keyed by coupon. */
export interface LegEvaluatedV1 {
    couponId: string;
    legId: string;
    fixtureId: string;
    resultVersion: number;
    outcome: LegOutcome;
    evaluatedAt: string;
}

export type LegOutcome = "won" | "lost" | "void";

/** Stage two: the coupon's settlement at SettlementVersion. A result correction produces a higher version; payout moves only the difference between TargetPayout and what it has already paid. */
export interface CouponSettledV1 {
    couponId: string;
    punterId: string;
    settlementVersion: number;
    outcome: CouponOutcome;
    stake: Money;
    effectiveOdds: number;
    targetPayout: Money;
    settledAt: string;
}

export type CouponOutcome = "won" | "lost" | "void";

export interface StuckCouponV1 {
    couponId: string;
    reason: string;
    evaluatedLegs: number;
    legCount: number;
    repaired: boolean;
    detectedAt: string;
}

/** A payout travelling the retry ladder (5s, 1m, 15m) or parked on the dead-letter topic. It carries the settlement outcome so every retry derives exactly the same wallet idempotency key as the first attempt. */
export interface PayoutAttemptV1 {
    couponId: string;
    punterId: string;
    settlementVersion: number;
    outcome: CouponOutcome;
    targetPayout: Money;
    step: PayoutStep;
    attempt: number;
    lastError: string | null;
    firstAttemptAt: string;
}

/** Named, not positional: a retried payout resumes at the step it failed on, whatever steps are added later. */
export type PayoutStep = "computeDelta" | "creditWallet" | "recordPayment";

export interface PayoutCompletedV1 {
    couponId: string;
    punterId: string;
    settlementVersion: number;
    delta: Money;
    paidToDate: Money;
    completedAt: string;
}

/** A detector opened an incident. Kind is one of stuckCoupon, walletOutage, poisonMessage, duplicateSettlement. */
export interface IncidentRaisedV1 {
    incidentId: string;
    kind: string;
    subject: string;
    summary: string;
    openedAt: string;
}

/** An incident changed status (diagnosing, awaitingApproval, diagnosisFailed, resolved). */
export interface IncidentUpdatedV1 {
    incidentId: string;
    kind: string;
    subject: string;
    status: string;
    rootCause: string | null;
    updatedAt: string;
}

/** An operator-approved remediation ran against the platform. */
export interface RemediationExecutedV1 {
    incidentId: string;
    actionId: string;
    actionType: string;
    target: string;
    succeeded: boolean;
    outcome: string;
    decidedBy: string;
    executedAt: string;
}

/** A customer registered and passed the age gate. Carries no personal details; those stay in identity. */
export interface UserRegisteredV1 {
    userId: string;
    brand: string;
    country: string;
    currency: string;
    registeredAt: string;
}

export interface EmailVerifiedV1 {
    userId: string;
    verifiedAt: string;
}

/** An account moved between active, suspended, closed and self-excluded. ChangedBy is a user id or a service name. */
export interface AccountStatusChangedV1 {
    userId: string;
    previousStatus: AccountStatus;
    status: AccountStatus;
    reason: string;
    changedBy: string;
    changedAt: string;
}

export type AccountStatus = "active" | "suspended" | "closed" | "selfExcluded";

/** Sessions to end at once. A null SessionId means every session of the user (suspension, self-exclusion, closure). */
export interface SessionRevokedV1 {
    userId: string;
    sessionId: string | null;
    reason: string;
    revokedAt: string;
}

/** One audited action, written through the owning service's outbox in the same transaction as the change. Before and After are JSON snapshots of the changed fields, null when not applicable. */
export interface AuditRecordedV1 {
    auditId: string;
    service: string;
    actor: string;
    action: string;
    subjectType: string;
    subjectId: string;
    before: string | null;
    after: string | null;
    correlationId: string;
    occurredAt: string;
}

/** Asks notifications to send one message. Recipient is the address for the channel (empty for in-app). Marketing messages are suppressed for restricted or self-excluded customers; service messages are not. */
export interface NotificationRequestedV1 {
    notificationId: string;
    userId: string;
    channel: NotificationChannel;
    template: string;
    recipient: string;
    data: { [key: string]: string; };
    isMarketing: boolean;
    requestedAt: string;
}

export type NotificationChannel = "email" | "push" | "inApp";

/** RFC 7807 problem details plus a stable machine code and the request's correlation id. */
export interface ErrorEnvelope {
    type: string;
    title: string;
    status: number;
    code: string;
    correlationId: string;
    detail: string | null;
    instance: string | null;
    errors: FieldError[] | null;
}

export interface FieldError {
    field: string;
    code: string;
    message: string;
}
