# HumbleContractVerifier
_It's dangerous to go alone! Take this._

A series of tools to help validate goose-style masterchef contracts. 
Current checks:
1. Token is owned by masterchef
2. Masterchef is owned by Timelock
3. Enumerates all existing pools and their variables to reveal any suspicious settings or hidden pools
4. Flags methods which appear to contain withdrawal fees

Features:
1. Estimates farm launch date and time
2. Saves contracts to disk for further review

## Sample Output
```
[19:28:09.23] D:\PackageTest\content HumbleVerifier.exe --help
HumbleVerifier 1.0.0
Copyright (C) 2021 HumbleVerifier

  -m, --mconly    (Default: false) Validate Masterchef contract only

  -a, --apikey    Required. ApiKey for calling BscScan. Get yours at https://bscscan.com/myapikey

  --help          Display this help screen.

  --version       Display version information.

  value pos. 0    Required. Address of the token (or masterchef in the case of -m) contract. E.g. 0xf1f1024f4f36001e5c0a1ad3ef5d0cc7c01af5fb

  value pos. 1    (Default: 56) Specify the chain ID. 1=Ethereum, 56=BSC, 137=Polygon, 250=Fantom, 43114=Avalanche.


[19:30:22.81] D:\PackageTest\content HumbleVerifier.exe 0xf1f1024f4f36001e5c0a1ad3ef5d0cc7c01af5fb -a 5X6TXMTGMXS9KQ49MEZHN7BS52B5X4NI24
Token Name: WISH
Token Address: 0xf1f1024f4f36001e5c0a1ad3ef5d0cc7c01af5fb
MasterChef Address: 0x141ccd27bbca7524dc6b7eea1eb41e097d7e832d
StartBlock: 6578730
Estimated launch date (PST): Already launched
TimeLock Address: 0x8027866264596aabacad63e7cedf8ab3c0cc72ec
!!! WARNING: Timelock is not a contract !!!
!!! WARNING: Methods with possible withdrawal fee detected !!!
add(uint256 _allocPoint, address _lpToken, uint16 _depositFeeBP, uint16 _withdrawFeeBP, bool _withUpdate)
set(uint256 _pid, uint256 _allocPoint, uint16 _depositFeeBP, uint16 _withdrawFeeBP, bool _withUpdate)
Pool info:
| pair      | lpToken                                    | allocPoint | lastRewardBlock | accWishPerShare            | depositFeeBP | withdrawFeeBP |
|-----------|--------------------------------------------|------------|-----------------|----------------------------|--------------|---------------|
| BUSD-WISH | 0xb3bca418222cfe2647ebdf1936329256b455816b | 7000       | 7887417         | 50296173875603             | 0            | 10000         |
| WBNB-WISH | 0xb30bd01a136ff9ac9103cb3e2bb96ea9f239e326 | 4000       | 7895537         | 1113775664323839           | 0            | 10000         |
| WBNB-BUSD | 0x1b96b92314c44b159149f7e0303511fb2fc4774f | 200        | 7824638         | 341523778676001            | 100          | 10000         |
| USDT-BUSD | 0xc15fa3e22c912a276550f3e5fe3b0deb87b55acd | 200        | 7674325         | 15120391258277             | 100          | 10000         |
| BTCB-WBNB | 0x7561eee90e24f3b348e1087a005f78b4c8453524 | 200        | 7889214         | 96010515608613796          | 100          | 10000         |
| ETH-WBNB  | 0x70d8929d04b60af4fb9b58713ebcf18765ade422 | 200        | 7889222         | 35564553223465054          | 100          | 10000         |
| DAI-BUSD  | 0x3ab77e40340ab084c3e23be8e5a6f7afed9d41dc | 200        | 7818939         | 939772334837494            | 100          | 10000         |
| USDC-BUSD | 0x680dd100e4b394bda26a59dd5c119a391e747d18 | 200        | 7851872         | 47859238570840755          | 100          | 10000         |
| DOT-WBNB  | 0xbcd62661a6b1ded703585d3af7d7649ef4dcdb5c | 200        | 7803871         | 216736480930545386         | 100          | 10000         |
| Cake-BUSD | 0x0ed8e0a2d99643e1e65cca22ed4424090b8b7458 | 100        | 7864506         | 3759480765496700           | 100          | 10000         |
| Cake-WBNB | 0xa527a61703d82139f8a06bc30097cc9caa2df5a6 | 100        | 7796978         | 1140797620990374           | 100          | 10000         |
| WISH      | 0xf1f1024f4f36001e5c0a1ad3ef5d0cc7c01af5fb | 1500       | 7869274         | 2375896521022              | 0            | 10000         |
| BUSD      | 0xe9e7cea3dedca5984780bafc599bd69add087d56 | 100        | 7739697         | 7117542089992              | 100          | 10000         |
| WBNB      | 0xbb4cdb9cbd36b01bd1cbaebf2de08d9173bc095c | 200        | 7864519         | 137131449257507838         | 100          | 10000         |
| USDT      | 0x55d398326f99059ff775485246999027b3197955 | 100        | 7683229         | 11565261045972             | 100          | 10000         |
| BTCB      | 0x7130d2a12b9bcbfae4f2634d864a1ee1ce3ead9c | 100        | 7665689         | 24094242706518153840       | 100          | 10000         |
| ETH       | 0x2170ed0880ac9a755fd29b2688956bd959f933f8 | 100        | 7665687         | 11533458198299879974338221 | 100          | 10000         |
| DAI       | 0x1af3f329e8be154074d8769d1ffa4ee058b1dbc3 | 100        | 7683108         | 6497585899954              | 100          | 10000         |
| USDC      | 0x8ac76a51cc950d9822d68b83fe1ad97b32cd580d | 100        | 7796985         | 4076963450698              | 100          | 10000         |
| DOT       | 0x7083609fce4d1d8dc0c979aab8c869ea2c873402 | 100        | 7406259         | 91924707333257             | 100          | 10000         |
| Cake      | 0x0e09fabb73bd3ade0a17ecc321fd13a19e81ce82 | 100        | 7179673         | 243814821336918            | 100          | 10000         |
| UNI       | 0xbf5140a22578168fd562dccf235e5d43a02ce9b1 | 10         | 6774830         | 24377159778314             | 100          | 10000         |
| AUTO      | 0xa184088a740c695e156f91f5cc086a06bb78b827 | 10         | 6844261         | 5942403610509529           | 100          | 10000         |
| BUNNY     | 0xc9849e6fdb743d08faee3e34dd2d1bc69ea11a51 | 10         | 7671960         | 36933800215515145281511041 | 100          | 10000         |
| BSCX      | 0x5ac52ee5b2a633895292ff6d8a89bb9190451587 | 10         | 6774830         | 7202788380013              | 100          | 10000         |


Saving contracts to: C:\Users\Humble\AppData\Local\Temp\WISH
Writing C:\Users\Humble\AppData\Local\Temp\WISH\WishToken.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\BEP20.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\IBEP20.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\Context.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\Ownable.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\SafeMath.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\MasterChef.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\WishToken1.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\BEP201.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\IBEP201.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\SafeBEP20.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\Context1.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\Ownable1.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\SafeMath1.sol
Writing C:\Users\Humble\AppData\Local\Temp\WISH\Address.sol
```
